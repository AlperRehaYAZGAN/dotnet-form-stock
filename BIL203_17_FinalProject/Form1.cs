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


namespace BIL203_17_FinalProject
{
    public partial class Form1 : Form
    {
        Data.Data data;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create Data class and assign
            var data = new Data.Data();
            String url = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            data.OpenConnection(url);

            // create table if not exist
            data.CreateTableIfNotExist();

            // set data 
            this.data = data;

            refreshDataGrid();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            var stock = new Class.Stock();
            stock.Marka = txtMarka.Text;
            stock.IlacAdi = txtIlacAdi.Text;
            stock.AlisTutari = Convert.ToInt32(txtAlisTutari.Text);
            stock.StokAdedi = Convert.ToInt32(txtStokAdedi.Text);
            // dtAlisTarihi is DateTimePicker
            stock.AlisTarihi = dtAlisTarihi.Value;
            // cbSGKDurum is enum "Var" "Yok" so we can use form combo box index to get enum value
            stock.SGKDurum = (Class.SGKDurum)cbSgkDurum.SelectedIndex;

            // show prompt to "Id @id ve Name: @name verisini oluşturmak istediğinize emin misiniz?"
            var result = MessageBox.Show("Name: " + stock.Marka + " verisini oluşturmak istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }
            
            // if id is not null then update stock; otherwise insert stock
            data.InsertStock(stock);

            // notify user and clear text fields
            MessageBox.Show("Veri kaydedildi.");
            clearForm();
            refreshDataGrid();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            refreshDataGrid();
        }

        private void btngoruntule_Click(object sender, EventArgs e)
        {
            // if selected row is null, return; otherwise get selectoed row and fill stock object corresponding textfields.
            if (dataGridView1.SelectedRows.Count == 0) { return; }

            var row = dataGridView1.SelectedRows[0];
            var stock = new Class.Stock();
            stock.Id = Convert.ToInt32(row.Cells["id"].Value);
            stock.Marka = row.Cells["marka"].Value.ToString();
            stock.IlacAdi = row.Cells["ilacadi"].Value.ToString();
            stock.AlisTutari = Convert.ToInt32(row.Cells["alistutari"].Value);
            stock.StokAdedi = Convert.ToInt32(row.Cells["stokadedi"].Value);
            stock.AlisTarihi = Convert.ToDateTime(row.Cells["alistarihi"].Value);
            stock.SGKDurum = (Class.SGKDurum)Convert.ToInt32(row.Cells["sgkdurum"].Value);

            // fill text fields
            txtStokKodu.Text = stock.Id.ToString();
            txtMarka.Text = stock.Marka;
            txtIlacAdi.Text = stock.IlacAdi;
            txtAlisTutari.Text = stock.AlisTutari.ToString();
            txtStokAdedi.Text = stock.StokAdedi.ToString();
            dtAlisTarihi.Value = stock.AlisTarihi;
            cbSgkDurum.SelectedIndex = (int)stock.SGKDurum;

            // notify user
            MessageBox.Show("Id : " + stock.Id + " verisi seçildi.");
        }

        private void btnnotifyselected_Click(object sender, EventArgs e)
        {
            // if selected row is null, return; otherwise get selectoed row and fill stock object corresponding textfields.
            if (dataGridView1.SelectedRows.Count == 0) { return; }

            var row = dataGridView1.SelectedRows[0];
            var stock = new Class.Stock();
            stock.Id = Convert.ToInt32(row.Cells["id"].Value);
            stock.Marka = row.Cells["marka"].Value.ToString();
            stock.IlacAdi = row.Cells["ilacadi"].Value.ToString();
            stock.AlisTutari = Convert.ToInt32(row.Cells["alistutari"].Value);
            stock.StokAdedi = Convert.ToInt32(row.Cells["stokadedi"].Value);
            stock.AlisTarihi = Convert.ToDateTime(row.Cells["alistarihi"].Value);
            stock.SGKDurum = (Class.SGKDurum)Convert.ToInt32(row.Cells["sgkdurum"].Value);

            // fill text fields
            txtStokKodu.Text = stock.Id.ToString();
            txtMarka.Text = stock.Marka;
            txtIlacAdi.Text = stock.IlacAdi;
            txtAlisTutari.Text = stock.AlisTutari.ToString();
            txtStokAdedi.Text = stock.StokAdedi.ToString();
            dtAlisTarihi.Value = stock.AlisTarihi;
            cbSgkDurum.SelectedIndex = (int)stock.SGKDurum;
            
            // notify user
            MessageBox.Show("Id : " + stock.Id + " verisi seçildi.");
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (txtStokKodu.Text == "") { return; }

            var kodInt = Convert.ToInt32(txtStokKodu.Text);

            // "Id @id versini silmeye emin misin?"
            var result = MessageBox.Show("Id : " + kodInt + " Name: " + txtIlacAdi.Text + " verisini silmeye emin misin?", "Silme Onayı", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) { return; }
            
            data.DeleteStock(kodInt);

            // notify user and clear text fields
            MessageBox.Show("Id : " + kodInt + " verisi silindi.");
            clearForm();
            refreshDataGrid();
        }

        private void btnduzelt_Click(object sender, EventArgs e)
        {
            // if txtkod is null, return; otherwise update stock
            if (txtStokKodu.Text == "") {
                MessageBox.Show("Lütfen düzeltmek istediğiniz veriyi seçiniz (kod girdisi boş).");
                return; 
            }
            
            // Stock object
            var stock = new Class.Stock();
            stock.Id = Convert.ToInt32(txtStokKodu.Text);
            stock.Marka = txtMarka.Text;
            stock.IlacAdi = txtIlacAdi.Text;
            stock.AlisTutari = Convert.ToInt32(txtAlisTutari.Text);
            stock.StokAdedi = Convert.ToInt32(txtStokAdedi.Text);
            // dtAlisTarihi is DateTimePicker
            stock.AlisTarihi = dtAlisTarihi.Value;
            // cbSGKDurum is enum "Var" "Yok" so we can use form combo box index to get enum value
            stock.SGKDurum = (Class.SGKDurum)cbSgkDurum.SelectedIndex;

            data.UpdateStock(stock);

            // notify user and clear text fields
            MessageBox.Show("Name : " + stock.IlacAdi + " verisi düzeltildi.");
            clearForm();
            refreshDataGrid();
        }

        private void refreshDataGrid()
        {
            // sql data adapter for table stocks
            var adapter = new SqlDataAdapter("SELECT * FROM stocks", this.data.GetConn());
            var ds = new DataSet();

            // fill dataset
            adapter.Fill(ds, "stocks");

            // fill data grid view
            dataGridView1.DataSource = ds.Tables["stocks"];
        }

        private void clearForm()
        {
            txtStokKodu.Text = "";
            txtMarka.Text = "";
            txtIlacAdi.Text = "";
            txtAlisTutari.Text = "";
            txtStokAdedi.Text = "";
            dtAlisTarihi.Value = DateTime.Now;
            cbSgkDurum.SelectedIndex = 0;
        }

    }
}
