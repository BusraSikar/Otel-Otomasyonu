using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace otelOtomasyon
{
    public partial class Form2 : Form
    {
        public Form1 fr1;
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.14.0; Data Source=otel.accdb");

        public void odalar()
        {
            foreach (Control item in fr1.Controls)
            {
                if (item is Button)
                {
                    if (item.BackColor == Color.White)
                    {
                        comboBox1.Items.Add(item.Text);
                    }
                }
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            odalar();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select*from odabilgileri where oda'" + comboBox2.SelectedItem + "' ", baglanti);
            OleDbDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox12.Text = read["kat"].ToString();
                textBox1.Text = read[2].ToString();
                textBox2.Text = read[3].ToString();
                textBox3.Text = read[4].ToString();
                textBox9.Text = read[5].ToString();
            }

            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update odabilgileri set kat='" + textBox4.Text + "',yataksayisi='" + textBox3.Text + "', banyosayisi='" + textBox2.Text + "',cephe='" + textBox1.Text + "', gucret='" + textBox5.Text + "'  where oda='" + comboBox2.SelectedItem + "' ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("oda kaydı güncellendi");
            for (int i = 0; i < groupBox2.Controls.Count; i++)
            {
                if (groupBox2.Controls[i] is TextBox)
                {
                    groupBox2.Controls[i].Text = "";
                }
            }
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            fr1.odadurumu();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into odabilgileri(oda,kat,yataksayisi,banyosayisi,cephe,gucret,durumu) values('" + comboBox1.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','BOŞ')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Oda kaydı yapıldı", " Oda Kayıt");
            comboBox1.Text = "";
            //comboBox2.Text = "";

            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is TextBox)
                {
                    groupBox1.Controls[i].Text = "";
                }
            }

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            fr1.comboBox2.Items.Clear();
            fr1.odadurumu();
            odalar();

        }
    }
}
