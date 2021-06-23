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

using System.Net;

namespace oturumuaçıktutma
{
    public partial class FrmGiris : Form
    {
        SqlConnection con;

        SqlDataReader dr;

        SqlCommand com;

        string ip;
        void ipgetir()
        {
            foreach (IPAddress IP in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                ip = IP.ToString();
            }
        }

        void oturumaçma()
        {
            con = new SqlConnection("Server =.; Database = oturum; Integrated Security = True");

            con.Open();

            com = new SqlCommand("Select *from user_log where IP = '" + ip + "'", con);

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                if (dr[4].ToString() == "true")
                {

                    this.TopMost = false;
                    checkBox1.Checked = true;

                    timer1.Start();

                    FormAnasayfa fa = new FormAnasayfa();

                    fa.Show();
                }

                else
                {
                    checkBox1.Checked = false;
                }

               
            }

            con.Close();
        }

        void giriş()
        {
            con = new SqlConnection("Server =.; Database = oturum; Integrated Security = True");

            con.Open();

            com = new SqlCommand("Select *from user_log where K_ADI = '" + textBox1.Text + "' and ŞİFRE = '"+textBox2.Text+"'", con);

            dr = com.ExecuteReader();

            if (dr.Read())
            {

                durumgüncelle();

                FormAnasayfa fa = new FormAnasayfa();

                fa.Show();

                this.Hide();
            }

            else
            {
                MessageBox.Show("giriş bilgileri yanlış");
            }

            con.Close();

        }

        void durumgüncelle()
        {
            string güncelle = "update user_log set ŞİFRE =  @ŞİFRE, IP = @IP, DURUM = @DURUM where K_ADI = @K_ADI";

            con = new SqlConnection("Server =.; Database = oturum; Integrated Security = True");

            com = new SqlCommand(güncelle, con);

            com.Parameters.AddWithValue("@K_ADI", textBox1.Text);

            com.Parameters.AddWithValue("@ŞİFRE", textBox2.Text);

            com.Parameters.AddWithValue("@IP", ip);

            if (checkBox1.Checked)
            {
                com.Parameters.AddWithValue("@DURUM", "true");
            }

            else
            {
                com.Parameters.AddWithValue("@DURUM", "false");
            }
            

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public FrmGiris()
        {
            InitializeComponent();
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            ipgetir();

            giriş();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmKayıtOl fko = new FrmKayıtOl();

            fko.Show();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            ipgetir();

            oturumaçma();
        }

        int süre = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            süre--;

            if (süre == 0)
            {
                timer1.Stop();

                this.Hide();
            }
        }
    }
}
