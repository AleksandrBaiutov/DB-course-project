using System;
using System.Windows.Forms;

namespace db_project2
{
    public partial class AddAuthorForm : Form
    {
        private readonly DataAccess _dataAccess;
        private readonly string _selectedBookISBN;

        public AddAuthorForm(DataAccess dataAccess, string selectedBookISBN)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            _selectedBookISBN = selectedBookISBN;
        }

        private async void btnAddAuthor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Пожалуйста, введите полное имя автора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime birthDate = dtpBirthDate.Value.Date;

            try
            {
                await _dataAccess.AddAuthorAsync(_selectedBookISBN, txtFullName.Text, birthDate, txtPenName.Text);
                MessageBox.Show("Автор успешно добавлен к книге.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления автора к книге: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}