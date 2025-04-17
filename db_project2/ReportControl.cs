using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace db_project2
{
    public partial class ReportPage : UserControl
    {
        private readonly DataAccess _dataAccess;

        public ReportPage(DataAccess dataAccess)
        {
            InitializeComponent();
            _dataAccess = dataAccess;
            LoadReportDataAsync();
        }

        private async Task LoadReportDataAsync(string titleFilter = null)
        {
            try
            {
                List<BookReportItem> reportData = await _dataAccess.GetBookReportAsync(titleFilter);
                dgvReport.DataSource = reportData;

                dgvReport.DataSource = reportData.Select(item => new
                {
                    Название = item.Title,
                    Всего_экземпляров = item.TotalCopies,
                    Возвращено = item.ReturnedCopies,
                    Не_возвращено = item.NotReturnedCopies,
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            LoadReportDataAsync(searchTextBox.Text);
        }
    }
}