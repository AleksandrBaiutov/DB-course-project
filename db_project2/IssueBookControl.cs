using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_project2
{
    public partial class IssueBookControl : UserControl
    {
        private readonly DataAccess _dataAccess;
        private List<Book> _allBooks;
        private List<Reader> _allReaders;
        private Book _selectedBook;
        private Copy _selectedCopy;
        private Reader _selectedReader;

        public IssueBookControl(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadBooksAsync();
            await LoadReadersAsync();
            EnableIssueButton();
        }

        private async Task LoadBooksAsync(string searchTerm = null, string searchBy = "Название")
        {
            try
            {
                _allBooks = await _dataAccess.GetBooksAsync(searchTerm, searchBy);
                UpdateBookListDisplay(_allBooks);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBookListDisplay(List<Book> books)
        {
            dgvBooks.DataSource = books.Select(b => new
            {
                b.ISBN,
                Название = b.Title,
                Автор = string.Join(", ", b.Authors.Select(a => a.FullName))
            }).ToList();
            dgvBooks.Columns["ISBN"].Visible = false;
        }

        private async Task LoadReadersAsync(string searchTerm = null)
        {
            try
            {
                _allReaders = await _dataAccess.GetReadersAsync(searchTerm);
                UpdateReaderListDisplay(_allReaders);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке читателей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateReaderListDisplay(List<Reader> readers)
        {
            dgvReaders.DataSource = readers.Select(r => new
            {
                r.ReaderId,
                ФИО = r.FullName,
                Телефон = r.PhoneNumber
            }).ToList();
            dgvReaders.Columns["ReaderId"].Visible = false;
        }

        private async void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string isbn = dgvBooks.Rows[e.RowIndex].Cells["ISBN"].Value.ToString();
                _selectedBook = _allBooks.FirstOrDefault(b => b.ISBN == isbn);
                if (_selectedBook != null)
                {
                    await LoadCopiesAsync(_selectedBook.ISBN);
                }
            }
        }

        private async Task LoadCopiesAsync(string isbn)
        {
            try
            {
                if (_selectedBook != null && _selectedBook.Copies.Count == 0)
                {
                    _selectedBook.Copies = await _dataAccess.GetCopiesAsync(isbn);
                }
                UpdateCopyListDisplay(_selectedBook?.Copies);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке копий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCopyListDisplay(List<Copy> copies)
        {
            cmbCopies.Items.Clear();
            if (copies != null && copies.Any())
            {
                cmbCopies.Items.AddRange(copies.Where(c => c.Returned).Select(c => $"ID: {c.CopyId}, Год: {c.DatePublished.Year}, Страна: {c.PublishCountry}").ToArray());
                cmbCopies.Enabled = true;
            }
            else
            {
                cmbCopies.Enabled = false;
            }
            _selectedCopy = null;
            EnableIssueButton();
        }

        private void cmbCopies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCopies.SelectedIndex != -1 && _selectedBook != null && _selectedBook.Copies != null)
            {
                string selectedCopyText = cmbCopies.SelectedItem.ToString();
                try
                {
                    int copyId = int.Parse(selectedCopyText.Split(new string[] { "ID: ", ", " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    _selectedCopy = _selectedBook.Copies.FirstOrDefault(c => c.CopyId == copyId);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show($"Ошибка формата строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Дополнительная обработка ошибки, например, сброс _selectedCopy
                    _selectedCopy = null;
                }
                catch (IndexOutOfRangeException ex)
                {
                    MessageBox.Show($"Ошибка индекса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _selectedCopy = null;
                }
            }
            else
            {
                _selectedCopy = null;
            }
            EnableIssueButton();
        }

        private void dgvReaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int readerId = (int)dgvReaders.Rows[e.RowIndex].Cells["ReaderId"].Value;
                _selectedReader = _allReaders.FirstOrDefault(r => r.ReaderId == readerId);
            }
            else
            {
                _selectedReader = null;
            }
            EnableIssueButton();
        }

        private void EnableIssueButton()
        {
            btnIssueBook.Enabled = _selectedBook != null && _selectedCopy != null && _selectedReader != null;
        }

        private async void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (_selectedBook != null && _selectedCopy != null && _selectedReader != null)
            {
                try
                {
                    bool success = await _dataAccess.IssueBookAsync(_selectedCopy.CopyId, _selectedReader.ReaderId);
                    if (success)
                    {
                        MessageBox.Show("Книга успешно выдана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCopiesAsync(_selectedBook.ISBN);
                        _selectedCopy = null;
                        cmbCopies.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при выдаче книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выдаче книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу, копию и читателя.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void txtBookSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtBookSearch.Text;
            string searchBy = cmbBookSearchBy.SelectedItem?.ToString() ?? "Название";
            await LoadBooksAsync(searchTerm, searchBy);
        }

        private async void txtReaderSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtReaderSearch.Text;
            await LoadReadersAsync(searchTerm);
        }

        private void IssueBookControl_Load(object sender, EventArgs e)
        {
            cmbBookSearchBy.Items.AddRange(new string[] { "Название", "Автор" });
            cmbBookSearchBy.SelectedIndex = 0;
        }

    }
}