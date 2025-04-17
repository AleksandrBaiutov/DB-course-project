using System;
using System.Windows.Forms;

namespace db_project2
{
    public partial class AddBookForm : Form
    {
        private readonly DataAccess _dataAccess;

        public AddBookForm(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
        }

        private async void btnAddBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("Пожалуйста, введите ISBN книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Пожалуйста, введите название книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await _dataAccess.AddBookAsync(txtISBN.Text, txtTitle.Text, txtDescription.Text);
                MessageBox.Show("Книга успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}