using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using System.Text;
using System.Drawing.Printing;

namespace TODOLIST
{
    public partial class Form1 : XtraForm
    {

        //readonly SqlConnection baglanti = new SqlConnection("Server=192.168.1.22;Initial Catalog=TODOLIST;Integrated Security=False;User Id=sa;Password=Sifre123");
        //readonly SqlConnection baglanti = new SqlConnection("Data Source = RAMAZAN; Initial Catalog = TODOLIST; Integrated Security = True");
        readonly List<DateTime> tarihler = new List<DateTime>();
        PrintDocument printDoc = new PrintDocument();

        private Dictionary<Keys, SimpleButton> shortcutButtons = new Dictionary<Keys, SimpleButton>();
        private SqlConnection baglanti;


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = "dd.MM.yyyy";
            textBox3.ForeColor = Color.LightGray;
            try
            {

                string setupDosyaAdi = @"C:\\Users\\Ramaz\\source\\repos\\TODOLIST\\TODOLIST\\obj\\Debug\\BağlantıYolu.txt";
                string setupDosyaYolu = System.IO.Path.Combine(Application.StartupPath, setupDosyaAdi);
                string connectionString = File.ReadAllText(setupDosyaYolu);
                 baglanti = new SqlConnection(connectionString);
                baglanti.Open();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata meydana geldi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            GetTarihlerFromSQL();
            GorevGetir();

            shortcutButtons.Add(Keys.K, simpleButton1);
            shortcutButtons.Add(Keys.A, simpleButton5);
            shortcutButtons.Add(Keys.G, btnGuncelle);
            shortcutButtons.Add(Keys.S, simpleButton2);
            shortcutButtons.Add(Keys.E, simpleButton3);
            shortcutButtons.Add(Keys.C, simpleButton4);
            shortcutButtons.Add(Keys.Q, btnEXCELkyt);
            shortcutButtons.Add(Keys.W, btnTxtKayit);
            shortcutButtons.Add(Keys.R, btnPDFkyt);
            shortcutButtons.Add(Keys.T, btnHTMLkyt);
            shortcutButtons.Add(Keys.Y, btnYazıcı);
           
        }

        public void GetTarihlerFromSQL()
        {

            tarihler.Clear();
            string query = "SELECT Tarih FROM Görev WHERE Tarih >= @BaslangicTarihi AND Tarih < @BitisTarihi";
            DateTime selectedDate = calendarControl1.SelectionStart;
            DateTime baslangicTarihi = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime bitisTarihi = baslangicTarihi.AddMonths(1);
            using (SqlCommand command = new SqlCommand(query, baglanti))
            {
                command.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
                command.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime tarih = reader.GetDateTime(0);
                        tarihler.Add(tarih);
                    }
                    reader.Close();
                }
            }
        }

        private void calendarControl1_DateTimeChanged(object sender, EventArgs e)
        {
            GetTarihlerFromSQL();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listBoxValues = listBox1.Items.Cast<string>().ToList();
                string combinedValues = string.Join(", ", listBoxValues);
                DateTime selectedDate = calendarControl1.SelectionStart;
                string query = "INSERT INTO Görev (Tarih, Yapılacak_İş, Yapılacaklar_Listesi) VALUES (@Tarih, @YapilacakIs, @YapilacaklarListesi)";
                using (SqlCommand insertCommand = new SqlCommand(query, baglanti))
                {
                    insertCommand.Parameters.AddWithValue("@Tarih", selectedDate);
                    insertCommand.Parameters.AddWithValue("@YapilacakIs", textBox1.Text);
                    insertCommand.Parameters.AddWithValue("@YapilacaklarListesi", combinedValues);
                    insertCommand.ExecuteNonQuery();
                }
                XtraMessageBox.Show("Kayıt Eklendi", "Bilgi", MessageBoxButtons.OK);
                
            }
            catch (Exception hata)
            {
                XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
            }

            listBox1.Items.Clear();
            textBox1.Text = string.Empty;
            GetTarihlerFromSQL();
            GorevGetir();
            calendarControl1.Refresh();
            calendarControl1_DateTimeChanged(null, null);



        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox2.Text);
            textBox2.Text = string.Empty;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    SqlCommand komut = new SqlCommand("DELETE FROM Görev WHERE ID = @ID", baglanti);
                    komut.Parameters.AddWithValue("@ID", selectedRow.Cells["ID"].Value);
                    komut.ExecuteNonQuery();
                    XtraMessageBox.Show("Kayıt Silindi", "Bilgi", MessageBoxButtons.OK);
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Silinecek bir satır seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            GetTarihlerFromSQL();
            GorevGetir();

            calendarControl1.Refresh();
            calendarControl1_DateTimeChanged(null, null);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParseExact(textBox3.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tarih))
            {
                SqlCommand komut = new SqlCommand("SELECT * FROM Görev WHERE Tarih=@Tarih", baglanti);
                komut.Parameters.AddWithValue("@Tarih", tarih.Date);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Geçerli bir tarih giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string tarih = selectedRow.Cells["Tarih"].Value.ToString();
                string yapilacakIs = selectedRow.Cells["Yapılacak_İş"].Value.ToString();
                string yapilacaklarListesi = selectedRow.Cells["Yapılacaklar_Listesi"].Value.ToString();

                lblTarih.Text = DateTime.Parse(tarih).ToString("dd.MM.yyyy");
                textBox1.Text = yapilacakIs;
                listBox1.Items.Clear();
                listBox1.Items.AddRange(yapilacaklarListesi.Split(',').Select(item => item.Trim()).ToArray());
            }
            GetTarihlerFromSQL();
            calendarControl1.Refresh();
            //calendarControl1_DateTimeChanged(null, null);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    DateTime tarih = Convert.ToDateTime(selectedRow.Cells["Tarih"].Value);

                    List<string> listBoxValues = listBox1.Items.Cast<string>().ToList();
                    string combinedValues = string.Join(", ", listBoxValues);

                    SqlCommand komut = new SqlCommand("UPDATE Görev SET Tarih=@Tarih, Yapılacak_İş = @YapilacakIs, Yapılacaklar_Listesi = @YapilacaklarListesi WHERE ID = @ID", baglanti);
                    komut.Parameters.AddWithValue("@YapilacakIs", textBox1.Text);
                    komut.Parameters.AddWithValue("@YapilacaklarListesi", combinedValues);
                    komut.Parameters.AddWithValue("@Tarih", tarih);
                    komut.Parameters.AddWithValue("@ID", selectedRow.Cells["ID"].Value);
                    komut.ExecuteNonQuery();

                    XtraMessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK);
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Güncellenecek bir satır seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            GorevGetir();
        }

        private void calendarControl1_CustomDrawDayNumberCell_1(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (tarihler.Exists(x => x == e.Date.Date)) e.Style.BackColor = Color.Red;
        }

        private void calendarControl1_SelectionChanged(object sender, EventArgs e)
        {
            if (calendarControl1.SelectedRanges.Count() > 0)
            {
                GorevGetir();
            }
        }

        private void GorevGetir()
        {
            DateTime selectedDate = calendarControl1.SelectionStart.Date;
            lblTarih.Text = selectedDate.ToString("dd/MM/yyyy");
            string query = "SELECT * FROM Görev WHERE Tarih=@tarih";
            SqlCommand komut = new SqlCommand(query, baglanti);
            komut.Parameters.AddWithValue("@tarih", selectedDate);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (baglanti.State != ConnectionState.Closed)
            {
                baglanti.Close();
            }
        }

        private void btnTxtKayit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DateTime tarih = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);

                    // OpenFileDialog oluştur
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveFileDialog.FileName = $"{tarih:yyyy-MM-dd}_Kayitlar.txt"; // Varsayılan dosya adı
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Başlangıçta masaüstü gösterilsin

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string dosyaYolu = saveFileDialog.FileName;

                        using (StreamWriter writer = new StreamWriter(dosyaYolu))
                        {
                            writer.WriteLine($"Tarih: {tarih:dd.MM.yyyy}");
                            writer.WriteLine();

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                DateTime currentTarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
                                if (currentTarih.Date == tarih.Date)
                                {
                                    string yapilacakIs = row.Cells["Yapılacak_İş"].Value.ToString();
                                    string yapilacaklarListesi = row.Cells["Yapılacaklar_Listesi"].Value.ToString();
                                    List<string> yapilacaklarListe = yapilacaklarListesi.Split(',').Select(item => "*" + item.Trim()).ToList();

                                    writer.WriteLine($"Yapılacak İş: {yapilacakIs}");

                                    foreach (string yapilacak in yapilacaklarListe)
                                    {
                                        writer.WriteLine(yapilacak);
                                    }
                                    writer.WriteLine("--------------------------------------------------");
                                }
                            }
                        }

                        XtraMessageBox.Show("Veriler dosyaya kaydedildi.", "Bilgi", MessageBoxButtons.OK);
                    }
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEXCELkyt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DateTime tarih = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel Dosyaları|*.xlsx|Tüm Dosyalar|*.*";
                    saveFileDialog1.Title = "Excel Dosyasını Kaydet";
                    saveFileDialog1.FileName = $"{tarih:yyyy-MM-dd}_Kayitlar.xlsx";
                    // Kullanıcı bir konum seçerse
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {


                        Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                        Excel.Workbook workbook = excelApp.Workbooks.Add();
                        Excel.Worksheet worksheet = workbook.ActiveSheet;

                        int satir = 1;
                        string[] basliklar = { "Tarih", "Yapılacak İş", "Yapılacaklar Listesi" };
                        int baslikSutun = 1;
                        Color baslikArkaPlanRenk = Color.Black;
                        Color baslikYaziRenk = Color.White;

                        int baslikYaziBoyut = 20;

                        // Başlık satırını ekleyelim ve stilini belirleyelim
                        for (int i = 0; i < basliklar.Length; i++)
                        {
                            worksheet.Cells[satir, baslikSutun + i] = basliklar[i];
                            worksheet.Cells[satir, baslikSutun + i].Interior.Color = System.Drawing.ColorTranslator.ToOle(baslikArkaPlanRenk);
                            worksheet.Cells[satir, baslikSutun + i].Font.Color = System.Drawing.ColorTranslator.ToOle(baslikYaziRenk);
                            worksheet.Cells[satir, baslikSutun + i].Font.Size = baslikYaziBoyut;

                            worksheet.Cells[satir, baslikSutun + i].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            worksheet.Cells[satir, baslikSutun + i].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            worksheet.Cells[satir, baslikSutun + i].BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
                            worksheet.Columns.AutoFit();
                        }

                        // Her bir DataGridView satırını işleyelim
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            DateTime currentTarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
                            if (currentTarih.Date == tarih.Date)
                            {
                                string yapilacakIs = row.Cells["Yapılacak_İş"].Value.ToString();
                                string yapilacaklarListesi = row.Cells["Yapılacaklar_Listesi"].Value.ToString();

                                satir++;
                                worksheet.Cells[satir, 1] = currentTarih.ToString("dd.MM.yyyy");
                                worksheet.Cells[satir, 2] = yapilacakIs;

                                List<string> yapilacaklarListe = yapilacaklarListesi.Split(',').Select(item => "*" + item.Trim()).ToList();
                                worksheet.Cells[satir, 3] = string.Join(Environment.NewLine, yapilacaklarListe);

                                // Dikey ve yatay hizalamayı ortala
                                worksheet.Cells[satir, 1].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                worksheet.Cells[satir, 2].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                worksheet.Cells[satir, 3].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                worksheet.Cells[satir, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                worksheet.Cells[satir, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                worksheet.Cells[satir, 3].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                                worksheet.Cells[satir, 1].BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
                                worksheet.Cells[satir, 2].BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
                                worksheet.Cells[satir, 3].BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);
                            }
                        }

                        // Excel dosyasını kaydet ve Excel uygulamasını kapatma
                        workbook.SaveAs(saveFileDialog1.FileName);
                        workbook.Close();
                        excelApp.Quit();

                        XtraMessageBox.Show("Veriler dosyaya kaydedildi.", "Bilgi", MessageBoxButtons.OK);
                    }
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "dd.MM.yyyy")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "dd.MM.yyyy";
                textBox3.ForeColor = Color.LightGray;

            }
        }

        private void btnPDFkyt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DateTime tarih = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                    saveFileDialog.FileName = $"{tarih:yyyy-MM-dd}_Kayitlar.pdf";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string dosyaYolu = saveFileDialog.FileName;

                        var pdfWriter = new PdfWriter(dosyaYolu);
                        var pdfDoc = new PdfDocument(pdfWriter);
                        var doc = new Document(pdfDoc, PageSize.A4);



                        doc.Add(new Paragraph($"Tarih: {tarih:dd.MM.yyyy}"));
                        doc.Add(new Paragraph(""));


                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            DateTime currentTarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
                            if (currentTarih.Date == tarih.Date)
                            {
                                string yapilacakIs = row.Cells["Yapılacak_İş"].Value.ToString();
                                string yapilacaklarListesi = row.Cells["Yapılacaklar_Listesi"].Value.ToString();
                                List<string> yapilacaklarListe = yapilacaklarListesi.Split(',').Select(item => "*" + item.Trim()).ToList();

                                doc.Add(new Paragraph($"Yapılacak İş: {yapilacakIs}"));

                                foreach (string yapilacak in yapilacaklarListe)
                                {
                                    doc.Add(new Paragraph(yapilacak));
                                }

                                doc.Add(new Paragraph("--------------------------------------------------"));
                            }
                        }

                        doc.Close();

                        XtraMessageBox.Show("Veriler PDF olarak kaydedildi.", "Bilgi", MessageBoxButtons.OK);
                    }
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnYazıcı_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
        
        private void btnHTMLkyt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DateTime tarih = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "HTML Files (*.html)|*.html";
                    saveFileDialog.FileName = $"{tarih:yyyy-MM-dd}_Kayitlar.html"; // Varsayılan dosya adı
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Başlangıçta masaüstü gösterilsin

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string dosyaYolu = saveFileDialog.FileName;

                        using (StreamWriter writer = new StreamWriter(dosyaYolu))
                        {

                            writer.WriteLine($@"
<html>
<head>
<title>Veri Kaydı</title>
<style>
body
{{
    background: linear-gradient(to left top, black,white);
    font-family: Arial, sans-serif;    
}}
h2 {{
    font-size: 20px;
    color: blue; 
}}
h1 {{
    font-size: 24px;
    margin-bottom: 10px;
    color: red;
}}
p {{
    font-size: 14px;
    margin-bottom: 5px;
    position: relative;
    padding-left: 20px;
}}
p::before {{
    content: '\2022'; 
    position: absolute;
    left: 0;
}}

</style>
</head>
<body>
<h1>Tarih: {tarih:dd.MM.yyyy}</h1>
");

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                DateTime currentTarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
                                if (currentTarih.Date == tarih.Date)
                                {
                                    string yapilacakIs = row.Cells["Yapılacak_İş"].Value.ToString();
                                    string yapilacaklarListesi = row.Cells["Yapılacaklar_Listesi"].Value.ToString();
                                    List<string> yapilacaklarListe = yapilacaklarListesi.Split(',').Select(item => "" + item.Trim()).ToList();

                                    writer.WriteLine($"<h2>Yapılacak İş: {yapilacakIs}</h2>");

                                    foreach (string yapilacak in yapilacaklarListe)
                                    {
                                        writer.WriteLine($"<p>{yapilacak}</p>");
                                    }
                                }
                            }

                            writer.WriteLine(@"  
</body>
</html>
");
                        }

                        XtraMessageBox.Show("Veriler HTML olarak kaydedildi.", "Bilgi", MessageBoxButtons.OK);
                    }
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && shortcutButtons.ContainsKey(e.KeyCode))
            {
                shortcutButtons[e.KeyCode].PerformClick();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DateTime tarih = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);
                    StringBuilder sb = new StringBuilder();


                    sb.AppendLine($"Tarih: {tarih:dd.MM.yyyy}");
                    sb.AppendLine();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DateTime currentTarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
                        if (currentTarih.Date == tarih.Date)
                        {
                            string yapilacakIs = row.Cells["Yapılacak_İş"].Value.ToString();
                            string yapilacaklarListesi = row.Cells["Yapılacaklar_Listesi"].Value.ToString();
                            List<string> yapilacaklarListe = yapilacaklarListesi.Split(',').Select(item => "*" + item.Trim()).ToList();

                            sb.AppendLine($"Yapılacak İş: {yapilacakIs}");

                            foreach (string yapilacak in yapilacaklarListe)
                            {
                                sb.AppendLine(yapilacak);
                            }

                            sb.AppendLine("--------------------------------------------------");
                        }
                    }


                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {

                        printDoc.PrinterSettings = printDialog.PrinterSettings;
                    }
                    else
                    {
                        return;
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveFileDialog.FileName = $"{tarih:yyyy-MM-dd}_Kayitlar.txt";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string dosyaYolu = saveFileDialog.FileName;

                        File.WriteAllText(dosyaYolu, sb.ToString());


                        PrintDocument printDoc = new PrintDocument();
                        printDoc.DocumentName = "ToDoList_Print";

                        printDoc.PrintPage += (s, pe) =>
                        {
                            using (Font font = new Font("Arial", 12))
                            {
                                pe.Graphics.DrawString(sb.ToString(), font, Brushes.Black, new PointF(100, 100));
                            }
                        };

                        PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                        previewDialog.Document = printDoc;
                        previewDialog.ShowDialog();
                    }
                }
                catch (Exception hata)
                {
                    XtraMessageBox.Show("Hata meydana geldi: " + hata.Message);
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}