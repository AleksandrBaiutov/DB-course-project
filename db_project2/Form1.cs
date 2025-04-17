using System;
using System.Configuration;
using System.Windows.Forms;
using Npgsql;

namespace db_project2
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["LibraryDatabase"].ConnectionString;
        private readonly DataAccess _dataAccess; // Добавляем экземпляр DataAccess

        public Form1()
        {
            InitializeComponent();
            _dataAccess = new DataAccess(_connectionString); // Инициализируем DataAccess
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ShowError(new Exception("Введите логин или пароль."));
                return;
            }

            try
            {
                var authorizationConnectionString = $"{_connectionString}Username=authorization;Password=authorization_password;";

                using (var connection = new NpgsqlConnection(authorizationConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT t.full_name, t.role FROM public.checkpsw(@login, @password) AS t", connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string fullName = reader.GetString(reader.GetOrdinal("full_name"));
                                string role = reader.GetString(reader.GetOrdinal("role"));

                                ShowSuccess($"Успешная авторизация! Привет, {fullName} (Роль: {role}).");

                                var userConnectionString = $"{_connectionString}Username={login};Password={password};";

                                this.Hide();
                                MainForm mainForm = new MainForm(fullName, role, userConnectionString);
                                mainForm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                ShowError(new Exception("Неверный логин или пароль."));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowError(Exception ex)
        {
            string errorMessage = $"Ошибка: {ex.Message}\n\n";

            if (ex.InnerException != null)
            {
                errorMessage += $"Внутренняя ошибка: {ex.InnerException.Message}\n\n";
            }

            errorMessage += $"Трассировка стека:\n{ex.StackTrace}";

            errorLabel.Text = errorMessage;
            errorLabel.Visible = true;
            errorLabel.ForeColor = System.Drawing.Color.Red;
        }

        private void ShowSuccess(string message)
        {
            errorLabel.Text = message;
            errorLabel.Visible = true;
            errorLabel.ForeColor = System.Drawing.Color.Green;
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var registrationForm = new RegistrationForm(_dataAccess))
            {
                if (registrationForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Регистрация прошла успешно. Теперь вы можете войти.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}