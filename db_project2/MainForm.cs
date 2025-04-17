using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Forms;

namespace db_project2
{
    public partial class MainForm : Form
    {
        private readonly DataAccess _dataAccess;
        private readonly string _userConnectionString;
        private readonly string _userFullName;
        private readonly string _userRole;

        public MainForm(string fullName, string role, string userConnectionString)
        {
            InitializeComponent();
            _userFullName = fullName;
            _userRole = role;
            _userConnectionString = userConnectionString;
            _dataAccess = new DataAccess(_userConnectionString);

            InitializeUserInterface();
        }

        private void InitializeUserInterface()
        {
            tabControl1.TabPages.Clear();

            TabPage readerTabPage = new TabPage("Книги");
            ReaderPageControl readerPageControl = new ReaderPageControl(_dataAccess);
            readerTabPage.Controls.Add(readerPageControl);
            readerPageControl.Dock = DockStyle.Fill;
            tabControl1.TabPages.Add(readerTabPage);

            if (_userRole == "Сотрудник" || _userRole == "Руководство")
            {
                TabPage issueBookTabPage = new TabPage("Выдача книг");
                IssueBookControl issueBookPageControl = new IssueBookControl(_dataAccess);
                issueBookTabPage.Controls.Add(issueBookPageControl);
                issueBookPageControl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(issueBookTabPage);

                TabPage returnBookPage = new TabPage("Возврат книг");
                ReturnBookControl returnBookControl = new ReturnBookControl(_dataAccess);
                returnBookPage.Controls.Add(returnBookControl);
                returnBookControl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(returnBookPage);

                TabPage addRemoveTabPage = new TabPage("Управление каталогом");
                AddRemoveControl addRemoveControl = new AddRemoveControl(_dataAccess);
                addRemoveTabPage.Controls.Add(addRemoveControl);
                addRemoveControl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(addRemoveTabPage);
            }

            if (_userRole == "Руководство")
            {
                TabPage manageUsersPage = new TabPage("Назначение сотрудников");
                ManageUsersControl manageUsersControl = new ManageUsersControl(_dataAccess);
                manageUsersPage.Controls.Add(manageUsersControl);
                manageUsersControl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(manageUsersPage);

                TabPage bookReportTabPage = new TabPage("Отчеты по книгам");
                ReportPage reportPageControl = new ReportPage(_dataAccess);
                bookReportTabPage.Controls.Add(reportPageControl);
                reportPageControl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(bookReportTabPage);
            }
        }
    }
}