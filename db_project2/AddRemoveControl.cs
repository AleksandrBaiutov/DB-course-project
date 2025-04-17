using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_project2
{
    public partial class AddRemoveControl : UserControl
    {
        private readonly DataAccess _dataAccess;
        private string _selectedBookISBN;

        public AddRemoveControl(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            LoadBooks();
            UpdateButtonsState();
        }

        private async Task LoadBooks(string searchText = null)
        {
            try
            {
                dgvBooks.DataSource = await _dataAccess.GetBooksAsync(searchText);
                // Показываем только название книги
                foreach (DataGridViewColumn column in dgvBooks.Columns)
                {
                    column.Visible = (column.Name == "Title");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCopiesForSelectedBook(string searchText = null)
        {
            if (_selectedBookISBN != null)
            {
                try
                {
                    dgvCopies.DataSource = await _dataAccess.GetCopiesAsync(_selectedBookISBN);
                    foreach (DataGridViewColumn column in dgvCopies.Columns)
                    {
                        column.Visible = (column.Name == "CopyId");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки копий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                dgvCopies.DataSource = null;
            }
        }

        private async Task LoadAuthorsForSelectedBook(string searchText = null)
        {
            if (_selectedBookISBN != null)
            {
                try
                {
                    dgvAuthors.DataSource = await _dataAccess.GetAuthorsByISBNAsync(_selectedBookISBN, searchText);
                    foreach (DataGridViewColumn column in dgvAuthors.Columns)
                    {
                        column.Visible = column.Name == "FullName";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки авторов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                dgvAuthors.DataSource = null;
            }
        }

        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                _selectedBookISBN = dgvBooks.SelectedRows[0].Cells["isbn"].Value?.ToString();
                LoadCopiesForSelectedBook(txtSearchCopies.Text); 
                LoadAuthorsForSelectedBook(txtSearchAuthors.Text);
            }
            else
            {
                _selectedBookISBN = null;
                dgvCopies.DataSource = null;
                dgvAuthors.DataSource = null;
            }
            UpdateButtonsState();
        }

        private void dgvCopies_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void dgvAuthors_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            btnAddCopy.Enabled = !string.IsNullOrEmpty(_selectedBookISBN);
            btnAddAuthor.Enabled = !string.IsNullOrEmpty(_selectedBookISBN);
            btnDeleteBook.Enabled = dgvBooks.SelectedRows.Count > 0;
            btnDeleteCopy.Enabled = dgvCopies.SelectedRows.Count > 0;
            btnDeleteAuthor.Enabled = dgvAuthors.SelectedRows.Count > 0;
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            using (var form = new AddBookForm(_dataAccess))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks(txtSearchBooks.Text);
                }
            }
        }

        private void btnAddCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedBookISBN))
            {
                using (var form = new AddCopyForm(_dataAccess, _selectedBookISBN))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadCopiesForSelectedBook(txtSearchCopies.Text);
                    }
                }
            }
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedBookISBN))
            {
                using (var form = new AddAuthorForm(_dataAccess, _selectedBookISBN))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadAuthorsForSelectedBook(txtSearchAuthors.Text);
                    }
                }
            }
        }

        private async void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                string isbnToDelete = dgvBooks.SelectedRows[0].Cells["ISBN"].Value?.ToString();
                if (!string.IsNullOrEmpty(isbnToDelete))
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту книгу?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            await _dataAccess.DeleteBookAsync(isbnToDelete);
                            LoadBooks(txtSearchBooks.Text);
                            dgvCopies.DataSource = null;
                            dgvAuthors.DataSource = null;
                            UpdateButtonsState();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка удаления книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void btnDeleteCopy_Click(object sender, EventArgs e)
        {
            if (dgvCopies.SelectedRows.Count > 0)
            {
                if (int.TryParse(dgvCopies.SelectedRows[0].Cells["CopyId"].Value?.ToString(), out int copyIdToDelete))
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту копию?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            await _dataAccess.DeleteCopyAsync(copyIdToDelete);
                            LoadCopiesForSelectedBook(txtSearchCopies.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка удаления копии: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void btnDeleteAuthor_Click(object sender, EventArgs e)
        {
            if (dgvAuthors.SelectedRows.Count > 0)
            {
                if (int.TryParse(dgvAuthors.SelectedRows[0].Cells["AuthorId"].Value?.ToString(), out int authorIdToDelete))
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого автора из книги?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            await _dataAccess.DeleteAuthorFromBookAsync(_selectedBookISBN, authorIdToDelete);
                            LoadAuthorsForSelectedBook(txtSearchAuthors.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка удаления автора: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void txtSearchBooks_TextChanged(object sender, EventArgs e)
        {
            LoadBooks(txtSearchBooks.Text);
        }

        private void txtSearchCopies_TextChanged(object sender, EventArgs e)
        {
            LoadCopiesForSelectedBook(txtSearchCopies.Text);
        }

        private void txtSearchAuthors_TextChanged(object sender, EventArgs e)
        {
            LoadAuthorsForSelectedBook(txtSearchAuthors.Text);
        }
    }
}