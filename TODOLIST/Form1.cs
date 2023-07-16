using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace TODOLIST
{
    public partial class Form1 : XtraForm
    {
        ///readonly SqlConnection baglanti = new SqlConnection("Server=192.168.1.22;Initial Catalog=TODOLIST;Integrated Security=False;User Id=sa;Password=Sifre123");
        readonly SqlConnection baglanti = new SqlConnection("Data Source = RAMAZAN; Initial Catalog = TODOLIST; Integrated Security = True");
        readonly List<DateTime> tarihler = new List<DateTime>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            GetTarihlerFromSQL();
            GorevGetir();
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
                    string masaustuKlasoru = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string klasorAdi = "Görevler";
                    string klasorYolu = Path.Combine(masaustuKlasoru, klasorAdi);


                    if (!Directory.Exists(klasorYolu))
                        Directory.CreateDirectory(klasorYolu);

                    string dosyaYolu = Path.Combine(klasorYolu, $"{tarih:yyyy-MM-dd}_Kayitlar.txt");

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
                    string masaustuKlasoru = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string klasorAdi = "Görevler";
                    string klasorYolu = Path.Combine(masaustuKlasoru, klasorAdi);


                    if (!Directory.Exists(klasorYolu))// kalsör yoksa buradan oluşturulacak klasör adında değişiklik yaparsakta kullanılacak
                        Directory.CreateDirectory(klasorYolu);

                    string dosyaAdi = $"{tarih:yyyy-MM-dd}_Kayitlar.xlsx";
                    string dosyaYolu = Path.Combine(klasorYolu, dosyaAdi);

                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add();
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

                    int satir = 1;

                    worksheet.Cells[satir, 1] = "Tarih";
                    worksheet.Cells[satir, 2] = "Yapılacak İş";
                    worksheet.Cells[satir, 3] = "Yapılacaklar Listesi";

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
                        }
                    }

                    // Excel dosyasını kaydet ve Excel uygulamasını kapatma
                    workbook.SaveAs(dosyaYolu);
                    workbook.Close();
                    excelApp.Quit();

                    XtraMessageBox.Show("Veriler dosyaya kaydedildi.", "Bilgi", MessageBoxButtons.OK);
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
    }
}