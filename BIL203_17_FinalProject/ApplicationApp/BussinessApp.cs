using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL203_17_FinalProject.Application
{
    internal class BussinessApp
    {

        public bool StokAdetKontrolEt(int adet)
        {
            if (adet > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
