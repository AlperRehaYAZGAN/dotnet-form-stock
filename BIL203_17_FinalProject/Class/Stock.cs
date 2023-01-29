using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL203_17_FinalProject.Class
{

    // Stock class implements IStock
    internal class Stock : IStock
    {
        // Fields
        public int Id;
        public string Marka;
        public string IlacAdi;
        public int AlisTutari;
        public int StokAdedi;
        public DateTime AlisTarihi;
        public SGKDurum SGKDurum;

        // constructor
        public Stock() {}
        
        // constructor
        public Stock(int id, string marka, string ilacAdi, int alisTutari, int stokAdedi, DateTime alisTarihi, SGKDurum sgkDurum)
        {
            Id = id;
            Marka = marka;
            IlacAdi = ilacAdi;
            AlisTutari = alisTutari;
            StokAdedi = stokAdedi;
            AlisTarihi = alisTarihi;
            SGKDurum = sgkDurum;
        }
    }
}
