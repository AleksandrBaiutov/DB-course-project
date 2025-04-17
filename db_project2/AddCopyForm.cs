using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_project2
{
    public partial class AddCopyForm : Form
    {
        private readonly DataAccess _dataAccess;
        private string _bookISBN;

        public AddCopyForm(DataAccess dataAccess, string bookISBN = null)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            _bookISBN = bookISBN;
            LoadBookISBNs();

            if (!string.IsNullOrEmpty(_bookISBN))
            {
                cbISBN.SelectedItem = _bookISBN;
                cbISBN.Enabled = false;
            }
        }

        private async Task LoadBookISBNs()
        {
            try
            {
                var books = await _dataAccess.GetBooksAsync();
                cbISBN.DataSource = books.Select(b => b.ISBN).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ISBN книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAddCopy_Click(object sender, EventArgs e)
        {
            if (cbISBN.SelectedItem == null && string.IsNullOrEmpty(_bookISBN))
            {
                MessageBox.Show("Пожалуйста, выберите ISBN книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPublishCountry.Text))
            {
                MessageBox.Show("Пожалуйста, введите страну публикации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedISBN = _bookISBN ?? cbISBN.SelectedItem.ToString();

            try
            {
                await _dataAccess.AddCopyAsync(selectedISBN, dtpDatePublished.Value.Date, txtPublishCountry.Text);
                MessageBox.Show("Копия успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления копии: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}