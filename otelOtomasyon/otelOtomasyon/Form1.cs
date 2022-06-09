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

namespace otelOtomasyon
{
    public partial class Form1 : Form
    {

        public Form2 fr2 = new Form2();

        public Form1()
        {
            InitializeComponent();
            fr2.fr1 = this;
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-RT6K1JI;Initial Catalog=Otomasyon;Integrated Security=True");

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
        public void odadurumu()
        {
            int sayi = 101;

            foreach (Control btn in Controls)
            {
                if (btn is Button)
                {
                    if (btn.Name != "button44" && btn.Name != "button43")
                    {

                        btn.BackColor = Color.White;
                        btn.Text = "ODA-" + sayi.ToString();
                        sayi++;


                    }
                }

            }


            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from odabilgileri",baglanti);

            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control oda in Controls)
                {
                    if(oda is Button)
                    {
                        if (read["oda"].ToString()==oda.Text && read["durumu"].ToString()=="BOŞ")
                        {
                            oda.BackColor = Color.Green;


                            comboBox3.Items.Add(read["oda"].ToString());
                            fr2.comboBox1.Items.Add(read["oda"].ToString());
                        }
                        if (read["oda"].ToString() == oda.Text && read["durumu"].ToString() == "DOLU")
                        {
                            oda.BackColor = Color.Red;
                        }
                    }
                }
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            foreach (Control renkdeğişikliği in Controls)
            {
                if (renkdeğişikliği is Button)
                {


                    renkdeğişikliği.Click += Renkdeğişikliği_Click;

                }
            }
            odadurumu();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from odabilgileri where oda='"+comboBox1.SelectedItem+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {


                textBox19.Text = read["kat"].ToString();
                textBox20.Text = read[2].ToString();
                textBox23.Text = read[3].ToString();
                textBox24.Text = read[4].ToString();
                textBox22.Text = read[5].ToString();
            }

                baglanti.Close();


            }

        private void button43_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kayitbilgileri(ad,soyad,adres,telefon,email,oda,kat,gtarih,ctarih,gun,gucret,tutar,odemeturu,aciklama)values ('"+textBox4.Text+ "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + comboBox1.Text + "','" + textBox9.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + comboBox2.Text + "','" + textBox13.Text + "',)", baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update odabilgileri set durumu='DOLU' where oda= '"+comboBox1.SelectedItem+"'", baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi","Kayıt");
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.Items.Clear();
            odadurumu();
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is TextBox)
                {
                    groupBox1.Controls[i].Text = "";
                }
            }

        }

        private void button44_Click(object sender, EventArgs e)
        {
            fr2.ShowDialog();
            fr2.odalar();
            odadurumu();
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            foreach (Control renkdeğişikliği in Controls)
            {
                if (renkdeğişikliği is Button)
                {


                    renkdeğişikliği.Click += Renkdeğişikliği_Click;

                }
            }
            odadurumu();
        }

        private void Renkdeğişikliği_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.BackColor == Color.Red)
            {
                DialogResult cevap = MessageBox.Show("Oda çıkışı yapılsın mı?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (cevap == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("delete*from kayitbilgileri where oda='" + b.Text + "' ", baglanti);
                    komut.ExecuteNonQuery();

                    SqlCommand komut2 = new SqlCommand("update odabilgileri set durumu='BOŞ' where oda='" + b.Text + "' ", baglanti);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Oda çıkışı yapıldı");
                    comboBox2.Items.Clear();
                    fr2.comboBox1.Items.Clear();
                    fr2.comboBox2.Items.Clear();
                    odadurumu();
                }
            }

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

            TimeSpan gun = new TimeSpan();
            gun = DateTime.Parse(dateTimePicker2.Text) - DateTime.Parse(dateTimePicker1.Text);
            textBox4.Text = gun.TotalDays.ToString();
            textBox5.Text = (double.Parse(textBox4.Text) * double.Parse(textBox11.Text)).ToString();
        }

       // private void button43_Click(object sender, EventArgs e)
        //{

            //odadurumu();
            //fr2.odalar();
            //fr2.ShowDialog();
        //}

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click_1(object sender, EventArgs e)
        {

        }
    }
    }
    

