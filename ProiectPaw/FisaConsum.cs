using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    internal class FisaConsum
    {
        private int codFisa;
        private string numeProdus;
        private int cantitateConsumata;
        private double pretProdus;
        private DateTime dataEliberare;

        public FisaConsum(int codFisa, string numeProdus, int cantitateConsumata, DateTime dataEliberare, double pretProdus)
        {
            this.CodFisa = codFisa;
            this.NumeProdus = numeProdus;
            this.CantitateConsumata = cantitateConsumata;
            this.DataEliberare = dataEliberare;
            this.PretProdus = pretProdus;
        }

        public int CodFisa { get => codFisa; set => codFisa = value; }
        public string NumeProdus { get => numeProdus; set => numeProdus = value; }
        public int CantitateConsumata { get => cantitateConsumata; set => cantitateConsumata = value; }
        public DateTime DataEliberare { get => dataEliberare; set => dataEliberare = value; }
        public double PretProdus { get => pretProdus; set => pretProdus = value; }
    }
}
