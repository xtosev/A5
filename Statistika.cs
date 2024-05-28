using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace _4EIT_A5
{
    public partial class Statistika : Form
    {
        SqlConnection konekcija = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\A5.mdf;Integrated Security=True");

        public Statistika()
        {
            InitializeComponent();
        }

        private void buttonPrikazi_Click(object sender, EventArgs e)
        {
            SqlCommand komanda = new SqlCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = 
                @"SELECT a.Dan as Dan, (COUNT(r.DeteID) - 
                COUNT (CASE WHEN r.Prisustvo = 0 THEN 1 ELSE NULL END)) AS 'Broj dece'
                FROM Aktivnost AS a, Registar_Aktivnosti AS r
                WHERE a.AktivnostID = r.AktivnostID
                GROUP BY a.Dan";
            SqlDataAdapter adapter = new SqlDataAdapter(komanda);
            DataTable dt = new DataTable();
            try
            {
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                chart1.DataSource = dt;
                chart1.Series[0].XValueMember = "Dan";
                chart1.Series[0].YValueMembers = "Broj dece";
                chart1.Series[0].IsValueShownAsLabel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doslo je do greske! "+ ex.Message);
            }
            finally
            {
                adapter.Dispose();
                dt.Dispose();
            }
        }

        private void buttonIzadji_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
