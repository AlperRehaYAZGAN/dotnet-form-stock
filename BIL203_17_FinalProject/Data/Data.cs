using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIL203_17_FinalProject.Class;

namespace BIL203_17_FinalProject.Data
{
    internal class Data
    {
        // connection inner object
        private SqlConnection conn;

        // constructor
        public Data() {}

        public SqlConnection GetConn() { return conn; }

        // open connection
        public void OpenConnection(String url)
        {
            // System.Data.SqlClient 
            var conn = new SqlConnection(url);
            // open connection
            conn.Open();

            // set conn to data class
            this.conn = conn;
        }

        // create table if not exist  (id is primary key)
        public void CreateTableIfNotExist()
        {
            // id primary key auto increment
            try
            {
                /*
                // drop table stocks first
                var sqlDrop = "IF OBJECT_ID('dbo.stocks', 'U') IS NOT NULL DROP TABLE dbo.stocks";
                var cmdDrop = new SqlCommand(sqlDrop, conn);
                cmdDrop.ExecuteNonQuery();
                */

                var sql = "IF OBJECT_ID('dbo.stocks', 'U') IS NULL CREATE TABLE dbo.stocks (id int IDENTITY(1,1) PRIMARY KEY, marka varchar(255), ilacadi varchar(255), alistutari float, stokadedi int, alistarihi date, sgkdurum varchar(255))";
                var cmd = new SqlCommand(sql, this.conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        // insert stock
        public void InsertStock(Stock stock)
        {
           try {
                // insert stock
                var cmd = new SqlCommand("INSERT INTO stocks (marka, ilacadi, alistutari, stokadedi, alistarihi, sgkdurum) VALUES (@marka, @ilacadi, @alistutari, @stokadedi, @alistarihi, @sgkdurum)", this.conn);
                cmd.Parameters.AddWithValue("@marka", stock.Marka);
                cmd.Parameters.AddWithValue("@ilacadi", stock.IlacAdi);
                cmd.Parameters.AddWithValue("@alistutari", stock.AlisTutari);
                cmd.Parameters.AddWithValue("@stokadedi", stock.StokAdedi);
                cmd.Parameters.AddWithValue("@alistarihi", stock.AlisTarihi);
                cmd.Parameters.AddWithValue("@sgkdurum", stock.SGKDurum);
                cmd.ExecuteNonQuery();
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // update stock
        public void UpdateStock(Stock stock)
        {
            try
            {
                // update stock
                var cmd = new SqlCommand("UPDATE stocks SET marka = @marka, ilacadi = @ilacadi, alistutari = @alistutari, stokadedi = @stokadedi, alistarihi = @alistarihi, sgkdurum = @sgkdurum WHERE id = @id", this.conn);
                cmd.Parameters.AddWithValue("@id", stock.Id);
                cmd.Parameters.AddWithValue("@marka", stock.Marka);
                cmd.Parameters.AddWithValue("@ilacadi", stock.IlacAdi);
                cmd.Parameters.AddWithValue("@alistutari", stock.AlisTutari);
                cmd.Parameters.AddWithValue("@stokadedi", stock.StokAdedi);
                cmd.Parameters.AddWithValue("@alistarihi", stock.AlisTarihi);
                cmd.Parameters.AddWithValue("@sgkdurum", stock.SGKDurum);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // delete stock (id int)
        public void DeleteStock(int id)
        {
            try
            {
                // delete stock
                var cmd = new SqlCommand("DELETE FROM stocks WHERE id = @id", this.conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public int GetStockCount()
        {
            try
            {
                // get stock count (Internetten buldum)
                var cmd = new SqlCommand("SELECT COUNT(*) FROM stocks", this.conn);
                var count = (int)cmd.ExecuteScalar();
                return count;
            }
            catch (Exception e)
            {
                MessageBox.Show("GetStockCount:" + e.Message);
                return 0;
            }
        }

        public int GetStockBrandCount()
        {
            try
            {
               // Internetten buldum.
                var cmd = new SqlCommand("SELECT COUNT(DISTINCT marka) FROM stocks", this.conn);
                var count = (int)cmd.ExecuteScalar();
                return count;
            }
            catch (Exception e)
            {
                MessageBox.Show("GetStockBrandCount:" + e.Message);
                return 0;
            }
        }

        public int GetStockTotalPrice()
        {
            try
            {
                // get all total price of socks by select 
                var cmd = new SqlCommand("SELECT (SUM(alistutari * stokadedi)) FROM stocks", this.conn);
                // print db response
                try
                {
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Stokta hiç veri olmadığı için toplam alış  tutarı çekilemedi." + e.Message);
                    return 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("GetStockTotalPrice:" + e.Message);
                    return 0;
            }
        }
    }
}