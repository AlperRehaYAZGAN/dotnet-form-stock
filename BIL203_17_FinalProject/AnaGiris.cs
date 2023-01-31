using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIL203_17_FinalProject
{
    public partial class AnaGiris : Form
    {
        Data.Data data;

        public AnaGiris()
        {
            InitializeComponent();
        }

        private void AnaGiris_Load(object sender, EventArgs e)
        {
            // create Data class and assign
            var data = new Data.Data();
            String url = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            data.OpenConnection(url);
            this.data = data;

            // create table if not exist
            data.CreateTableIfNotExist();

            // verileri veritabanindan cek
            verileriGetir();
        }

        private void btnStokGit_Click(object sender, EventArgs e)
        {
            Form1 stokForm = new Form1();
            stokForm.Show();
        }

        private void btnVerileriYenile_Click(object sender, EventArgs e)
        {
            // verileri veritabanindan cek
            verileriGetir();
        }
        private void verileriGetir()
        {
            lblStokAdet.Text = this.data.GetStockCount().ToString() + " adet ürün";
            lblStokMarkaAdet.Text = this.data.GetStockBrandCount().ToString() + " adet marka";
            lblStokAlisTutar.Text = this.data.GetStockTotalPrice().ToString() + " TL";

        }
    }
}
