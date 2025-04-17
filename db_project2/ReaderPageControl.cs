using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_project2
{
    public partial class ReaderPageControl : UserControl
    {
        private readonly DataAccess _dataAccess;

        public ReaderPageControl(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;

            cmbSearchBy.Items.AddRange(new string[] { "Название", "Автор" });
            cmbSearchBy.SelectedIndex = 0;

            LoadBooks();
        }
        private async void LoadBooks(string searchTerm = null, string searchBy = null)
        {
            try
            {
                List<Book> books = await _dataAccess.GetBooksAsync(searchTerm, searchBy);
                dgvBooks.DataSource = books.Select(b => new
                {
                    b.ISBN,
                    Название = b.Title,
                    Автор = string.Join(", ", b.Authors.Select(a => a.FullName))
                }).ToList();
                dgvBooks.Columns["ISBN"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string isbn = dgvBooks.Rows[e.RowIndex].Cells["ISBN"].Value.ToString();
                await OpenBookDetailsForm(isbn);
            }
        }

        private async Task OpenBookDetailsForm(string isbn)
        {
            try
            {
                Book book = await _dataAccess.GetBookDetailsAsync(isbn);
                if (book != null)
                {
                    var bookDetailsForm = new BookDetailsForm(book);
                    bookDetailsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Книга не найдена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии деталей книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            string searchBy = cmbSearchBy.SelectedItem?.ToString();
            LoadBooks(searchTerm, searchBy);
        }
    }
}
