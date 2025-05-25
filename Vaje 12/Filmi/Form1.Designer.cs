namespace Filmi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelYear = new System.Windows.Forms.Label();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.labelDirector = new System.Windows.Forms.Label();
            this.textBoxDirector = new System.Windows.Forms.TextBox();
            this.buttonAddMovie = new System.Windows.Forms.Button();
            this.labelChangeYear = new System.Windows.Forms.Label();
            this.textBoxChangeYear = new System.Windows.Forms.TextBox();
            this.labelChangeDelta = new System.Windows.Forms.Label();
            this.textBoxChangeDelta = new System.Windows.Forms.TextBox();
            this.buttonApplyChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // comboBoxColumn
            this.comboBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(12, 12);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(200, 21);
            this.comboBoxColumn.TabIndex = 0;

            // textBoxSearch
            this.textBoxSearch.Location = new System.Drawing.Point(220, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(200, 20);
            this.textBoxSearch.TabIndex = 1;

            // buttonSearch
            this.buttonSearch.Location = new System.Drawing.Point(430, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Isci";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);

            // dataGridView1
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(760, 300);
            this.dataGridView1.TabIndex = 3;

            // Add Movie Controls
int baseY = 370;
int spacingY = 30;

this.labelTitle.Text = "Naslov:";
this.labelTitle.Location = new System.Drawing.Point(12, baseY);
this.labelTitle.Size = new System.Drawing.Size(50, 20);
this.labelTitle.Visible = false;

this.textBoxTitle.Location = new System.Drawing.Point(70, baseY);
this.textBoxTitle.Size = new System.Drawing.Size(250, 20);
this.textBoxTitle.Visible = false;

this.labelYear.Text = "Leto:";
this.labelYear.Location = new System.Drawing.Point(340, baseY);
this.labelYear.Size = new System.Drawing.Size(40, 20);
this.labelYear.Visible = false;

this.textBoxYear.Location = new System.Drawing.Point(390, baseY);
this.textBoxYear.Size = new System.Drawing.Size(80, 20);
this.textBoxYear.Visible = false;

this.labelDirector.Text = "Reziser (ni nujno):";
this.labelDirector.Location = new System.Drawing.Point(490, baseY);
this.labelDirector.Size = new System.Drawing.Size(110, 20);
this.labelDirector.Visible = false;

this.textBoxDirector.Location = new System.Drawing.Point(610, baseY);
this.textBoxDirector.Size = new System.Drawing.Size(180, 20);
this.textBoxDirector.Visible = false;

this.buttonAddMovie.Text = "Dodaj film";
this.buttonAddMovie.Location = new System.Drawing.Point(12, baseY + spacingY);
this.buttonAddMovie.Size = new System.Drawing.Size(100, 25);
this.buttonAddMovie.Visible = false;
this.buttonAddMovie.Click += new System.EventHandler(this.buttonAddMovie_Click);

            // Change Ratings Controls
            this.labelChangeYear.Text = "Leto:";
            this.labelChangeYear.Location = new System.Drawing.Point(12, 410);
            this.labelChangeYear.Size = new System.Drawing.Size(40, 20);
            this.labelChangeYear.Visible = false;

            this.textBoxChangeYear.Location = new System.Drawing.Point(55, 407);
            this.textBoxChangeYear.Size = new System.Drawing.Size(80, 20);
            this.textBoxChangeYear.Visible = false;

            this.labelChangeDelta.Text = "Spremeni ocene:";
            this.labelChangeDelta.Location = new System.Drawing.Point(150, 410);
            this.labelChangeDelta.Size = new System.Drawing.Size(90, 20);
            this.labelChangeDelta.Visible = false;

            this.textBoxChangeDelta.Location = new System.Drawing.Point(245, 407);
            this.textBoxChangeDelta.Size = new System.Drawing.Size(80, 20);
            this.textBoxChangeDelta.Visible = false;

            this.buttonApplyChange.Text = "Spremeni";
            this.buttonApplyChange.Location = new System.Drawing.Point(340, 405);
            this.buttonApplyChange.Size = new System.Drawing.Size(75, 23);
            this.buttonApplyChange.Visible = false;

            this.buttonApplyChange.Click += new System.EventHandler(this.buttonApplyChange_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 450);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelYear);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.labelDirector);
            this.Controls.Add(this.textBoxDirector);
            this.Controls.Add(this.buttonAddMovie);
            this.Controls.Add(this.labelChangeYear);
            this.Controls.Add(this.textBoxChangeYear);
            this.Controls.Add(this.labelChangeDelta);
            this.Controls.Add(this.textBoxChangeDelta);
            this.Controls.Add(this.buttonApplyChange);
            this.Name = "Form1";
            this.Text = "Filmi";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelDirector;
        private System.Windows.Forms.TextBox textBoxDirector;
        private System.Windows.Forms.Button buttonAddMovie;
        private System.Windows.Forms.Label labelChangeYear;
        private System.Windows.Forms.TextBox textBoxChangeYear;
        private System.Windows.Forms.Label labelChangeDelta;
        private System.Windows.Forms.TextBox textBoxChangeDelta;
        private System.Windows.Forms.Button buttonApplyChange;
    }
}

