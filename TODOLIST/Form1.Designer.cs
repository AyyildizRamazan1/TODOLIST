
namespace TODOLIST
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.calendarControl1 = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuncelle = new DevExpress.XtraEditors.SimpleButton();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTxtKayit = new DevExpress.XtraEditors.SimpleButton();
            this.btnEXCELkyt = new DevExpress.XtraEditors.SimpleButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // calendarControl1
            // 
            this.calendarControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.calendarControl1.CalendarAppearance.Button.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.Button.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.ButtonPressed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.ButtonPressed.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellHighlighted.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.DayCellHighlighted.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.DayCellSpecialSelected.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.DayCellToday.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.DayCellToday.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.Header.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.Header.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.HeaderHighlighted.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.HeaderHighlighted.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.WeekDay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.WeekDay.Options.UseFont = true;
            this.calendarControl1.CalendarAppearance.WeekNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calendarControl1.CalendarAppearance.WeekNumber.Options.UseFont = true;
            this.calendarControl1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calendarControl1.Location = new System.Drawing.Point(18, 11);
            this.calendarControl1.Name = "calendarControl1";
            this.calendarControl1.Size = new System.Drawing.Size(362, 324);
            this.calendarControl1.TabIndex = 2;
            this.calendarControl1.SelectionChanged += new System.EventHandler(this.calendarControl1_SelectionChanged);
            this.calendarControl1.CustomDrawDayNumberCell += new DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventHandler(this.calendarControl1_CustomDrawDayNumberCell_1);
            this.calendarControl1.DateTimeChanged += new System.EventHandler(this.calendarControl1_DateTimeChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 351);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1362, 419);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(516, 181);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(237, 33);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "Günlük Kayıtı Bitir";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(516, 259);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(237, 33);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "Görevi Sil";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.Location = new System.Drawing.Point(795, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(208, 30);
            this.textBox2.TabIndex = 10;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(794, 64);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(208, 41);
            this.simpleButton3.TabIndex = 11;
            this.simpleButton3.Text = "Yapılacaklar Listesine Ekle";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
            this.simpleButton4.Location = new System.Drawing.Point(795, 119);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(207, 41);
            this.simpleButton4.TabIndex = 12;
            this.simpleButton4.Text = "Yapılacak İşi Sil";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuncelle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGuncelle.ImageOptions.Image")));
            this.btnGuncelle.Location = new System.Drawing.Point(516, 220);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(237, 33);
            this.btnGuncelle.TabIndex = 17;
            this.btnGuncelle.Text = "Günü Güncelle";
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox3.Location = new System.Drawing.Point(516, 106);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(237, 30);
            this.textBox3.TabIndex = 14;
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
            this.simpleButton5.Location = new System.Drawing.Point(516, 142);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(237, 33);
            this.simpleButton5.TabIndex = 13;
            this.simpleButton5.Text = "Günü Ara";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 22);
            this.label4.TabIndex = 15;
            this.label4.Text = "Tarih Ara:";
            // 
            // lblTarih
            // 
            this.lblTarih.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTarih.AutoSize = true;
            this.lblTarih.Location = new System.Drawing.Point(568, 25);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(116, 22);
            this.lblTarih.TabIndex = 5;
            this.lblTarih.Text = "Seçilen Tarih";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "Yapılacak İş:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(516, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 30);
            this.textBox1.TabIndex = 4;
            // 
            // btnTxtKayit
            // 
            this.btnTxtKayit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTxtKayit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTxtKayit.ImageOptions.Image")));
            this.btnTxtKayit.Location = new System.Drawing.Point(795, 166);
            this.btnTxtKayit.Name = "btnTxtKayit";
            this.btnTxtKayit.Size = new System.Drawing.Size(208, 41);
            this.btnTxtKayit.TabIndex = 18;
            this.btnTxtKayit.Text = "TXT Kayıt";
            this.btnTxtKayit.Click += new System.EventHandler(this.btnTxtKayit_Click);
            // 
            // btnEXCELkyt
            // 
            this.btnEXCELkyt.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEXCELkyt.ImageOptions.Image")));
            this.btnEXCELkyt.Location = new System.Drawing.Point(794, 213);
            this.btnEXCELkyt.Name = "btnEXCELkyt";
            this.btnEXCELkyt.Size = new System.Drawing.Size(209, 41);
            this.btnEXCELkyt.TabIndex = 19;
            this.btnEXCELkyt.Text = "EXCEL Kayıt";
            this.btnEXCELkyt.Click += new System.EventHandler(this.btnEXCELkyt_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 22;
            this.listBox1.Location = new System.Drawing.Point(1009, 11);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(270, 334);
            this.listBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.Appearance.BackColor = System.Drawing.Color.LavenderBlush;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.ClientSize = new System.Drawing.Size(1362, 770);
            this.Controls.Add(this.btnEXCELkyt);
            this.Controls.Add(this.btnTxtKayit);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.calendarControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TO DO LIST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calendarControl1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.Controls.CalendarControl calendarControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.TextBox textBox2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton btnGuncelle;
        private System.Windows.Forms.TextBox textBox3;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SimpleButton btnTxtKayit;
        private DevExpress.XtraEditors.SimpleButton btnEXCELkyt;
        private System.Windows.Forms.ListBox listBox1;
    }
}

