namespace DenseIndex
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.PrimaryKey = new System.Windows.Forms.ColumnHeader();
            this.Name_ = new System.Windows.Forms.ColumnHeader();
            this.Surname = new System.Windows.Forms.ColumnHeader();
            this.PhoneNumber = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_surname = new System.Windows.Forms.TextBox();
            this.textBox_phone_num = new System.Windows.Forms.TextBox();
            this.textBox_key = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PrimaryKey,
            this.Name_,
            this.Surname,
            this.PhoneNumber});
            this.listView1.Location = new System.Drawing.Point(61, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(884, 452);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // PrimaryKey
            // 
            this.PrimaryKey.Text = "PrimaryKey";
            this.PrimaryKey.Width = 250;
            // 
            // Name_
            // 
            this.Name_.Text = "Name";
            this.Name_.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Name_.Width = 180;
            // 
            // Surname
            // 
            this.Surname.Text = "Surname";
            this.Surname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Surname.Width = 180;
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.Text = "PhoneNumber";
            this.PhoneNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PhoneNumber.Width = 245;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 567);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Delete record";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(632, 601);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 50);
            this.button2.TabIndex = 2;
            this.button2.Text = "Edit record";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(632, 532);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 50);
            this.button3.TabIndex = 3;
            this.button3.Text = "Add new record";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(75, 567);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(198, 50);
            this.button4.TabIndex = 4;
            this.button4.Text = "Find record by key";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(824, 522);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.PlaceholderText = "Name";
            this.textBox_name.Size = new System.Drawing.Size(173, 27);
            this.textBox_name.TabIndex = 5;
            this.textBox_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_name.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox_surname
            // 
            this.textBox_surname.Location = new System.Drawing.Point(824, 579);
            this.textBox_surname.Name = "textBox_surname";
            this.textBox_surname.PlaceholderText = "Surname";
            this.textBox_surname.Size = new System.Drawing.Size(173, 27);
            this.textBox_surname.TabIndex = 6;
            this.textBox_surname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_surname.TextChanged += new System.EventHandler(this.textBox_surname_TextChanged);
            // 
            // textBox_phone_num
            // 
            this.textBox_phone_num.Location = new System.Drawing.Point(824, 633);
            this.textBox_phone_num.Name = "textBox_phone_num";
            this.textBox_phone_num.PlaceholderText = "Phone number";
            this.textBox_phone_num.Size = new System.Drawing.Size(173, 27);
            this.textBox_phone_num.TabIndex = 7;
            this.textBox_phone_num.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_key
            // 
            this.textBox_key.Location = new System.Drawing.Point(75, 522);
            this.textBox_key.Name = "textBox_key";
            this.textBox_key.PlaceholderText = "Key";
            this.textBox_key.Size = new System.Drawing.Size(198, 27);
            this.textBox_key.TabIndex = 8;
            this.textBox_key.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 672);
            this.Controls.Add(this.textBox_key);
            this.Controls.Add(this.textBox_phone_num);
            this.Controls.Add(this.textBox_surname);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listView1;
        private ColumnHeader PrimaryKey;
        private ColumnHeader Name_;
        private ColumnHeader Surname;
        private ColumnHeader PhoneNumber;
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox textBox_name;
        private TextBox textBox_surname;
        private TextBox textBox_phone_num;
        private TextBox textBox_key;
    }
}