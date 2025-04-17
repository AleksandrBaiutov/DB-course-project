using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_project2
{
    public partial class ReturnBookControl : UserControl
    {
        private readonly DataAccess _dataAccess;
        private List<Borrowing> _borrowedBooks;

        public ReturnBookControl(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            LoadBorrowedBooksAsync();
        }

        private async Task LoadBorrowedBooksAsync()
        {
            try
            {
                _borrowedBooks = await _dataAccess.SearchBorrowedBooksAsync();
                await UpdateBorrowedBooksDisplayAsync(_borrowedBooks);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке взятых книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateBorrowedBooksDisplayAsync(List<Borrowing> borrowings)
        {
            var displayData = new List<object>();
            foreach (var b in borrowings)
            {
                string bookTitle = await _dataAccess.GetBookTitleByCopyIdAsync(b.CopyId);
                string readerFullName = await _dataAccess.GetReaderFullNameByIdAsync(b.ReaderId);
                displayData.Add(new
                {
                    b.BorrowingId,
                    b.CopyId,
                    b.ReaderId,
                    Название = bookTitle,
                    ФИО = readerFullName,
                    Дата_Взятия = b.BorrowDate,
                    Дата_Возврата = b.ReturnDate
                });
            }
            dgvBorrowedBooks.DataSource = displayData;
            if (dgvBorrowedBooks.Columns["BorrowingId"] != null)
            {
                dgvBorrowedBooks.Columns["BorrowingId"].Visible = false;
            }
            if (dgvBorrowedBooks.Columns["CopyId"] != null)
            {
                dgvBorrowedBooks.Columns["CopyId"].Visible = false;
            }
            if (dgvBorrowedBooks.Columns["ReaderId"] != null)
            {
                dgvBorrowedBooks.Columns["ReaderId"].Visible = false;
            }
        }

        private async void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (dgvBorrowedBooks.SelectedRows.Count > 0)
            {
                int borrowingId = (int)dgvBorrowedBooks.SelectedRows[0].Cells["BorrowingId"].Value;
                try
                {
                    bool success = await _dataAccess.ReturnBookAsync(borrowingId);
                    if (success)
                    {
                        MessageBox.Show("Книга успешно возвращена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadBorrowedBooksAsync();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при возврате книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при возврате книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для возврата.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void txtReaderSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtReaderSearch.Text;
            await SearchBorrowedBooksAsync(searchTerm, label1.Text);
        }

        private async void txtBookSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtBookSearch.Text;
            await SearchBorrowedBooksAsync(txtReaderSearch.Text, searchTerm);
        }

        private async Task SearchBorrowedBooksAsync(string readerSearchTerm, string bookSearchTerm)
        {
            try
            {
                var filteredBooks = await _dataAccess.SearchBorrowedBooksAsync(readerSearchTerm, bookSearchTerm);
                await UpdateBorrowedBooksDisplayAsync(filteredBooks);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}