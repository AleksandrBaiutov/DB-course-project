using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_project2
{
    public partial class ManageUsersControl : UserControl
    {
        private readonly DataAccess _dataAccess;
        private List<User> _users;

        public ManageUsersControl(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadUsersAsync();
            cmbNewRole.Items.AddRange(new string[] { "Читатель", "Сотрудник", "Руководитель" });
        }

        private async Task LoadUsersAsync(string searchTerm = null)
        {
            try
            {
                _users = await _dataAccess.GetUsersAsync(searchTerm);
                UpdateUsersDisplay(_users);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUsersDisplay(List<User> users)
        {
            dgvUsers.DataSource = users.Select(u => new
            {
                u.UserId,
                ФИО = u.FullName,
                Роль = u.Role,
                Логин = u.Login
            }).ToList();
            dgvUsers.Columns["UserId"].Visible = false;
        }

        private async void txtUserSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtUserSearch.Text;
            await LoadUsersAsync(searchTerm);
        }

        private async void btnSaveRole_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0 && cmbNewRole.SelectedItem != null)
            {
                int userId = (int)dgvUsers.SelectedRows[0].Cells["UserId"].Value;
                string newRole = cmbNewRole.SelectedItem.ToString();
                try
                {
                   bool success = await _dataAccess.UpdateUserRoleAsync(userId, newRole);
                   if (success)
                    {
                        MessageBox.Show("Роль пользователя успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadUsersAsync(txtUserSearch.Text);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении роли пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении роли пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя и новую роль.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}