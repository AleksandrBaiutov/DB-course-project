using System;
using System.Windows.Forms;

namespace db_project2
{
    public partial class RegistrationForm : Form
    {
        private readonly DataAccess _dataAccess;

        public RegistrationForm(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullName = txtFullName.Text;
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            try
            {
                int newUserId = await _dataAccess.RegisterUserAsync(fullName, login, password);
                MessageBox.Show($"Регистрация прошла успешно. Ваш ID: {newUserId}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}