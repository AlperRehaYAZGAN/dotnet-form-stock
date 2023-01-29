using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL203_17_FinalProject.Class
{
    public enum SGKDurum
    {
        YOK,
        VAR
    }
    
    internal class IStock
    {
        // Fields
        public int Id;
        public string Marka;
        public string IlacAdi;
        public int AlisTutari;
        public int StokAdedi;
        public DateTime AlisTarihi;
        public SGKDurum SGKDurum;
    }
}
