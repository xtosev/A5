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
    public partial class Aktivnost : Form
    {
        SqlConnection konekcija = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\A5.mdf;Integrated Security=True");
        
        public Aktivnost()
        {
            InitializeComponent();
        }

        private void Aktivnost_Load(object sender, EventArgs e)
        {
            PopuniLV();
            PopuniComboBox();
            cbDan.SelectedIndex = -1;
        }
        private void PopuniLV()
        {
            SqlCommand komanda = new SqlCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = "SELECT * FROM Aktivnost";
            SqlDataAdapter adapter = new SqlDataAdapter(komanda);
            DataTable dt = new DataTable();
            try
            {
                listView1.Items.Clear();
                adapter.Fill(dt);
                foreach (DataRow red in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(red[0].ToString());
                    if (red[0] != DBNull.Value)
                    { item.SubItems.Add(red[1].ToString()); }
                    else
                    { item.SubItems.Add("");
                    }
                    if (red[2] != DBNull.Value)
                    { item.SubItems.Add(red[2].ToString()); }
                    else
                    {
                        item.SubItems.Add("");
                    }
                    if (red[3] != DBNull.Value)
                    {
                        item.SubItems.Add(DateTime.Parse(red[3].ToString()).ToString("HH:mm"));
                        
                    }
                    else
                    {
                        item.SubItems.Add("");
                    }
                    if (red[4] != DBNull.Value)
                    {item.SubItems.Add(DateTime.Parse(red[4].ToString()).ToString("HH:mm")); }
                    else
                    {
                        item.SubItems.Add("");
                    }
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PopuniComboBox()
        {
            SqlCommand komanda = new SqlCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = "SELECT DISTINCT(Dan) FROM Aktivnost";
            SqlDataAdapter adapter = new SqlDataAdapter(komanda);
            DataTable dt = new DataTable();
            try
            {
                adapter.Fill(dt);
                cbDan.DataSource = dt;
                cbDan.DisplayMember = "Dan";
                cbDan.ValueMember = "Dan";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ObrisiKontole()
        {
            tbSifra.Text = "";
            tbNaziv.Text = "";
            cbDan.SelectedIndex = -1;
            tbPocetak.Text = "";
            tbZavrsetak.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            tbSifra.Text = listView1.SelectedItems[0].SubItems[0].Text;
            tbNaziv.Text = listView1.SelectedItems[0].SubItems[1].Text;
            cbDan.Text = listView1.SelectedItems[0].SubItems[2].Text;
            tbPocetak.Text= listView1.SelectedItems[0].SubItems[3].Text;
            tbZavrsetak.Text= listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void toolStripButtonStatistika_Click(object sender, EventArgs e)
        {
            Statistika prikaz = new Statistika();
            prikaz.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object dan = DBNull.Value, pocetak = DBNull.Value, zavrsetak = DBNull.Value;

            if (tbSifra.Text=="" || tbNaziv.Text=="")
            {
                MessageBox.Show("Sifra i naziv moraju biti uneseni", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int sifra = int.Parse(tbSifra.Text);
            if (tbPocetak.Text!="")
            {
                pocetak= DateTime.ParseExact(tbPocetak.Text, "HH:mm", null);
                
            }
            if (tbZavrsetak.Text != "")
            {
                zavrsetak = DateTime.ParseExact(tbZavrsetak.Text, "HH:mm", null);

            }
            if (cbDan.Text!="")
            {
                dan = cbDan.Text;
            }

            SqlCommand komanda = new SqlCommand("INSERT INTO Aktivnost(AktivnostID, NazivAktivnosti, Dan, Pocetak,Zavrsetak) VALUES(@AktivnostID, @NazivAktivnosti, @Dan, @Pocetak,@Zavrsetak)", konekcija);          
            komanda.Parameters.AddWithValue("@AktivnostID", sifra);
            komanda.Parameters.AddWithValue("@NazivAktivnosti", tbNaziv.Text);
            komanda.Parameters.AddWithValue("@Dan", dan);
            komanda.Parameters.AddWithValue("@Pocetak", pocetak);
            komanda.Parameters.AddWithValue("@Zavrsetak", zavrsetak);
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Usesno ste uneli aktivnost", "Upis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopuniLV();
                ObrisiKontole();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void toolStripLabelOAplikaciji_Click(object sender, EventArgs e)
        {
            Uputsvo prikaz = new Uputsvo();
            prikaz.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
