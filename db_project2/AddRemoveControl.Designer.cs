namespace db_project2
{
    partial class AddRemoveControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.dgvCopies = new System.Windows.Forms.DataGridView();
            this.dgvAuthors = new System.Windows.Forms.DataGridView();
            this.txtSearchBooks = new System.Windows.Forms.TextBox();
            this.txtSearchAuthors = new System.Windows.Forms.TextBox();
            this.txtSearchCopies = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnAddCopy = new System.Windows.Forms.Button();
            this.btnDeleteCopy = new System.Windows.Forms.Button();
            this.btnAddAuthor = new System.Windows.Forms.Button();
            this.btnDeleteAuthor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCopies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBooks
            // 
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(21, 76);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.RowTemplate.Height = 24;
            this.dgvBooks.Size = new System.Drawing.Size(240, 315);
            this.dgvBooks.TabIndex = 0;
            this.dgvBooks.SelectionChanged += new System.EventHandler(this.dgvBooks_SelectionChanged);
            // 
            // dgvCopies
            // 
            this.dgvCopies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCopies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCopies.Location = new System.Drawing.Point(286, 76);
            this.dgvCopies.Name = "dgvCopies";
            this.dgvCopies.RowHeadersWidth = 51;
            this.dgvCopies.RowTemplate.Height = 24;
            this.dgvCopies.Size = new System.Drawing.Size(240, 315);
            this.dgvCopies.TabIndex = 1;
            this.dgvCopies.SelectionChanged += new System.EventHandler(this.dgvCopies_SelectionChanged);
            // 
            // dgvAuthors
            // 
            this.dgvAuthors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuthors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuthors.Location = new System.Drawing.Point(555, 76);
            this.dgvAuthors.Name = "dgvAuthors";
            this.dgvAuthors.RowHeadersWidth = 51;
            this.dgvAuthors.RowTemplate.Height = 24;
            this.dgvAuthors.Size = new System.Drawing.Size(240, 315);
            this.dgvAuthors.TabIndex = 2;
            this.dgvAuthors.SelectionChanged += new System.EventHandler(this.dgvAuthors_SelectionChanged);
            // 
            // txtSearchBooks
            // 
            this.txtSearchBooks.Location = new System.Drawing.Point(21, 34);
            this.txtSearchBooks.Name = "txtSearchBooks";
            this.txtSearchBooks.Size = new System.Drawing.Size(240, 22);
            this.txtSearchBooks.TabIndex = 3;
            this.txtSearchBooks.TextChanged += new System.EventHandler(this.txtSearchBooks_TextChanged);
            // 
            // txtSearchAuthors
            // 
            this.txtSearchAuthors.Location = new System.Drawing.Point(555, 34);
            this.txtSearchAuthors.Name = "txtSearchAuthors";
            this.txtSearchAuthors.Size = new System.Drawing.Size(240, 22);
            this.txtSearchAuthors.TabIndex = 4;
            this.txtSearchAuthors.TextChanged += new System.EventHandler(this.txtSearchAuthors_TextChanged);
            // 
            // txtSearchCopies
            // 
            this.txtSearchCopies.Location = new System.Drawing.Point(286, 34);
            this.txtSearchCopies.Name = "txtSearchCopies";
            this.txtSearchCopies.Size = new System.Drawing.Size(240, 22);
            this.txtSearchCopies.TabIndex = 5;
            this.txtSearchCopies.TextChanged += new System.EventHandler(this.txtSearchCopies_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnAddBook
            // 
            this.btnAddBook.Location = new System.Drawing.Point(21, 410);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(118, 42);
            this.btnAddBook.TabIndex = 7;
            this.btnAddBook.Text = "Добавить книгу";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // btnDeleteBook
            // 
            this.btnDeleteBook.Location = new System.Drawing.Point(143, 410);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(118, 42);
            this.btnDeleteBook.TabIndex = 8;
            this.btnDeleteBook.Text = "Удалить книгу";
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            // 
            // btnAddCopy
            // 
            this.btnAddCopy.Location = new System.Drawing.Point(286, 410);
            this.btnAddCopy.Name = "btnAddCopy";
            this.btnAddCopy.Size = new System.Drawing.Size(118, 42);
            this.btnAddCopy.TabIndex = 9;
            this.btnAddCopy.Text = "Добавить копию";
            this.btnAddCopy.UseVisualStyleBackColor = true;
            this.btnAddCopy.Click += new System.EventHandler(this.btnAddCopy_Click);
            // 
            // btnDeleteCopy
            // 
            this.btnDeleteCopy.Location = new System.Drawing.Point(410, 410);
            this.btnDeleteCopy.Name = "btnDeleteCopy";
            this.btnDeleteCopy.Size = new System.Drawing.Size(118, 42);
            this.btnDeleteCopy.TabIndex = 10;
            this.btnDeleteCopy.Text = "Удалить копию";
            this.btnDeleteCopy.UseVisualStyleBackColor = true;
            this.btnDeleteCopy.Click += new System.EventHandler(this.btnDeleteCopy_Click);
            // 
            // btnAddAuthor
            // 
            this.btnAddAuthor.Location = new System.Drawing.Point(555, 410);
            this.btnAddAuthor.Name = "btnAddAuthor";
            this.btnAddAuthor.Size = new System.Drawing.Size(118, 42);
            this.btnAddAuthor.TabIndex = 11;
            this.btnAddAuthor.Text = "Добавить автора";
            this.btnAddAuthor.UseVisualStyleBackColor = true;
            this.btnAddAuthor.Click += new System.EventHandler(this.btnAddAuthor_Click);
            // 
            // btnDeleteAuthor
            // 
            this.btnDeleteAuthor.Location = new System.Drawing.Point(677, 410);
            this.btnDeleteAuthor.Name = "btnDeleteAuthor";
            this.btnDeleteAuthor.Size = new System.Drawing.Size(118, 42);
            this.btnDeleteAuthor.TabIndex = 12;
            this.btnDeleteAuthor.Text = "Удалить автора";
            this.btnDeleteAuthor.UseVisualStyleBackColor = true;
            this.btnDeleteAuthor.Click += new System.EventHandler(this.btnDeleteAuthor_Click);
            // 
            // AddRemoveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteAuthor);
            this.Controls.Add(this.btnAddAuthor);
            this.Controls.Add(this.btnDeleteCopy);
            this.Controls.Add(this.btnAddCopy);
            this.Controls.Add(this.btnDeleteBook);
            this.Controls.Add(this.btnAddBook);
            this.Controls.Add(this.txtSearchCopies);
            this.Controls.Add(this.txtSearchAuthors);
            this.Controls.Add(this.txtSearchBooks);
            this.Controls.Add(this.dgvAuthors);
            this.Controls.Add(this.dgvCopies);
            this.Controls.Add(this.dgvBooks);
            this.Name = "AddRemoveControl";
            this.Size = new System.Drawing.Size(824, 526);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCopies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.DataGridView dgvCopies;
        private System.Windows.Forms.DataGridView dgvAuthors;
        private System.Windows.Forms.TextBox txtSearchBooks;
        private System.Windows.Forms.TextBox txtSearchAuthors;
        private System.Windows.Forms.TextBox txtSearchCopies;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnAddCopy;
        private System.Windows.Forms.Button btnDeleteCopy;
        private System.Windows.Forms.Button btnAddAuthor;
        private System.Windows.Forms.Button btnDeleteAuthor;
    }
}
