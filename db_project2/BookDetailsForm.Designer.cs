namespace db_project2
{
    partial class BookDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCopies = new System.Windows.Forms.DataGridView();
            this.lblTitleValue = new System.Windows.Forms.Label();
            this.lblAuthorsValue = new System.Windows.Forms.Label();
            this.lblDescriptionValue = new System.Windows.Forms.Label();
            this.lblISBNValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCopies
            // 
            this.dgvCopies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCopies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCopies.Location = new System.Drawing.Point(37, 148);
            this.dgvCopies.Name = "dgvCopies";
            this.dgvCopies.RowHeadersWidth = 51;
            this.dgvCopies.RowTemplate.Height = 24;
            this.dgvCopies.Size = new System.Drawing.Size(720, 272);
            this.dgvCopies.TabIndex = 0;
            // 
            // lblTitleValue
            // 
            this.lblTitleValue.AutoSize = true;
            this.lblTitleValue.Location = new System.Drawing.Point(126, 9);
            this.lblTitleValue.Name = "lblTitleValue";
            this.lblTitleValue.Size = new System.Drawing.Size(44, 16);
            this.lblTitleValue.TabIndex = 1;
            this.lblTitleValue.Text = "label1";
            // 
            // lblAuthorsValue
            // 
            this.lblAuthorsValue.AutoSize = true;
            this.lblAuthorsValue.Location = new System.Drawing.Point(126, 34);
            this.lblAuthorsValue.Name = "lblAuthorsValue";
            this.lblAuthorsValue.Size = new System.Drawing.Size(44, 16);
            this.lblAuthorsValue.TabIndex = 2;
            this.lblAuthorsValue.Text = "label2";
            // 
            // lblDescriptionValue
            // 
            this.lblDescriptionValue.AutoSize = true;
            this.lblDescriptionValue.Location = new System.Drawing.Point(126, 62);
            this.lblDescriptionValue.Name = "lblDescriptionValue";
            this.lblDescriptionValue.Size = new System.Drawing.Size(44, 16);
            this.lblDescriptionValue.TabIndex = 3;
            this.lblDescriptionValue.Text = "label3";
            // 
            // lblISBNValue
            // 
            this.lblISBNValue.AutoSize = true;
            this.lblISBNValue.Location = new System.Drawing.Point(126, 89);
            this.lblISBNValue.Name = "lblISBNValue";
            this.lblISBNValue.Size = new System.Drawing.Size(44, 16);
            this.lblISBNValue.TabIndex = 4;
            this.lblISBNValue.Text = "label4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Название:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Автор:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Описание:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "ISBN";
            // 
            // BookDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblISBNValue);
            this.Controls.Add(this.lblDescriptionValue);
            this.Controls.Add(this.lblAuthorsValue);
            this.Controls.Add(this.lblTitleValue);
            this.Controls.Add(this.dgvCopies);
            this.Name = "BookDetailsForm";
            this.Text = "BookDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCopies;
        private System.Windows.Forms.Label lblTitleValue;
        private System.Windows.Forms.Label lblAuthorsValue;
        private System.Windows.Forms.Label lblDescriptionValue;
        private System.Windows.Forms.Label lblISBNValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}