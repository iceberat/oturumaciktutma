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
    public partial class FrmKayıtOl : Form
    {
        SqlConnection con;

        SqlCommand com;
        string ip;
        void ipgetir()
        {
            foreach (IPAddress IP in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                ip = IP.ToString();
            }
        }

        
        public FrmKayıtOl()
        {
            InitializeComponent();
        }

        private void btnkayıt_Click(object sender, EventArgs e)
        {

            ipgetir();

            string ekle = "insert into user_log (K_ADI, ŞİFRE, IP, DURUM) values (@K_ADI, @ŞİFRE, @IP, @DURUM)";

            con = new SqlConnection("Server =.; Database = oturum; Integrated Security = True");

            com = new SqlCommand(ekle, con);

            com.Parameters.AddWithValue("@K_ADI", textBox1.Text);

            com.Parameters.AddWithValue("@ŞİFRE", textBox2.Text);

            com.Parameters.AddWithValue("@IP", ip);

            com.Parameters.AddWithValue("@DURUM", "false");

            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("kayıt eklendi");
        }
    }
}
