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
    public partial class BookDetailsForm : Form
    {
        private readonly Book _book;

        public BookDetailsForm(Book book)
        {
            InitializeComponent();
            _book = book;
            DisplayBookDetails();
        }
        public class ReviewViewModel
        {
            public int Оценка { get; set; }
            public string Отзыв { get; set; }
            public string Дата { get; set; }
        }
        private void DisplayBookDetails()
        {
            lblTitleValue.Text = _book.Title;
            lblAuthorsValue.Text = string.Join(", ", _book.Authors.Select(a => a.FullName));
            lblDescriptionValue.Text = _book.Description;
            lblISBNValue.Text = _book.ISBN;

            dgvCopies.DataSource = _book.Reviews
                .Select(r => new ReviewViewModel
                {
                    Оценка = r.Rating,
                    Отзыв = r.Text,
                    Дата = r.Date.ToShortDateString()
                })
                .ToList();

            dgvCopies.ReadOnly = true;
            dgvCopies.Columns["Отзыв"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCopies.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}