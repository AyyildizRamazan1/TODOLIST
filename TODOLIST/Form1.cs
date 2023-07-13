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



namespace TODOLIST
{
    public partial class Form1 : XtraForm
    {

        List<DateTime> tarihler = new List<DateTime>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {         
            tarihler = GetTarihlerFromSQL();
            calendarControl1.DateTimeChanged += calendarControl1_DateTimeChanged;

            calendarControl1.Refresh();
            calendarControl1_DateTimeChanged(null, null);
        }
        public List<DateTime> GetTarihlerFromSQL()
        {

            List<DateTime> tarihListesi = new List<DateTime>();

            string connectionString = "Data Source=RAMAZAN;Initial Catalog=TODOLIST;Integrated Security=True";
            string query = "SELECT Tarih FROM Görev WHERE Tarih >= @BaslangicTarihi AND Tarih < @BitisTarihi";
            DateTime selectedDate = calendarControl1.SelectionStart;
            DateTime baslangicTarihi = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime bitisTarihi = baslangicTarihi.AddMonths(1);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
                command.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime tarih = reader.GetDateTime(0);

                    tarihListesi.Add(tarih);
                }

                reader.Close();
            }

            return tarihListesi;
        }

        private void calendarControl1_DateTimeChanged(object sender, EventArgs e)
        {
            tarihler=GetTarihlerFromSQL();
            DateTime selectedDate = calendarControl1.SelectionStart;
            lblTarih.Text = selectedDate.ToString("dd/MM/yyyy");

            DateTime baslangicTarihi = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime bitisTarihi = baslangicTarihi.AddMonths(1);

            string query = "SELECT * FROM Görev WHERE Tarih >= @BaslangicTarihi AND Tarih < @BitisTarihi";
            SqlCommand komut = new SqlCommand(query, baglantı);
            komut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
            komut.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Visible = false;

        }

        SqlConnection baglantı = new SqlConnection("Data Source=RAMAZAN;Initial Catalog=TODOLIST;Integrated Security=True");
        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglantı);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void calendarControl1_Click(object sender, EventArgs e)
        {
            
            DateTime selectedDate = calendarControl1.SelectionStart;
            lblTarih.Text = selectedDate.ToString("dd/MM/yyyy");
            DateTime baslangicTarihi = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            DateTime bitisTarihi = baslangicTarihi.AddMonths(1);
            string query = "SELECT * FROM Görev WHERE Tarih >= @BaslangicTarihi AND Tarih < @BitisTarihi";
            SqlCommand komut = new SqlCommand(query, baglantı);
            komut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
            komut.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=RAMAZAN;Initial Catalog=TODOLIST;Integrated Security=True";
            try
            {
                List<string> listBoxValues = listBox1.Items.Cast<string>().ToList();
                string combinedValues = string.Join(", ", listBoxValues);
                
                DateTime selectedDate = calendarControl1.SelectionStart;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Görev (Tarih, Yapılacak_İş, Yapılacaklar_Listesi) VALUES (@Tarih, @YapilacakIs, @YapilacaklarListesi)", connection);
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
            textBox1.Text = "";
            tarihler.Clear();
            tarihler = GetTarihlerFromSQL();
            calendarControl1.Refresh();
            calendarControl1_DateTimeChanged(null, null);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox2.Text);
            textBox2.Text = "";
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

                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Görev WHERE ID = @ID", baglantı);
                    komut.Parameters.AddWithValue("@ID", selectedRow.Cells["ID"].Value);
                    
                    komut.ExecuteNonQuery();
                    baglantı.Close();
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
            tarihler = GetTarihlerFromSQL();
            calendarControl1.Refresh();
            calendarControl1_DateTimeChanged(null, null);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DateTime tarih;
            if (DateTime.TryParseExact(textBox3.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tarih))
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM Görev WHERE Tarih LIKE   @Tarih  ", baglantı);
                komut.Parameters.AddWithValue("@Tarih", tarih.ToString("yyyy-MM-dd"));
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglantı.Close();
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
            tarihler = GetTarihlerFromSQL();
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

                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("UPDATE Görev SET Tarih=@Tarih, Yapılacak_İş = @YapilacakIs, Yapılacaklar_Listesi = @YapilacaklarListesi WHERE ID = @ID", baglantı);
                    komut.Parameters.AddWithValue("@YapilacakIs", textBox1.Text);
                    komut.Parameters.AddWithValue("@YapilacaklarListesi", combinedValues);
                    komut.Parameters.AddWithValue("@Tarih", tarih);
                    komut.Parameters.AddWithValue("@ID", selectedRow.Cells["ID"].Value);
                    komut.ExecuteNonQuery();
                    baglantı.Close();

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
        }

        private void calendarControl1_CustomDrawDayNumberCell_1(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {

            if (tarihler.Exists(x => x == e.Date.Date))
            {
                e.Style.BackColor = Color.Red;
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
