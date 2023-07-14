using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    internal class LotFabricatie : IComparable<LotFabricatie>
    {
        private int numarLot;
        private DateTime dataFabricatie;
        private string numeFabricant;
        private string[] informatiiSuplimentare;

        public LotFabricatie(int numarLot, DateTime dataFabricatie, string numeFabricant, string[] informatiiSuplimentare)
        {
            this.NumarLot = numarLot;
            this.DataFabricatie = dataFabricatie;
            this.NumeFabricant = numeFabricant;
            this.informatiiSuplimentare = informatiiSuplimentare;
        }
        public int NumarLot { get => numarLot; set => numarLot = value; }
        public DateTime DataFabricatie { get => dataFabricatie; set => dataFabricatie = value; }
        public string NumeFabricant { get => numeFabricant; set => numeFabricant = value; }
        public string[] InformatiiSuplimentare { get => informatiiSuplimentare; set => informatiiSuplimentare = value; }

        public int CompareTo(LotFabricatie other)
        {
            if (this.dataFabricatie > other.dataFabricatie)
            {
                return 1;
            }
            else
                if (this.dataFabricatie < other.dataFabricatie)
            {
                return -1;
            }
            return 0;
        }
        public string this[int index]
        {
            get
            {
                return InformatiiSuplimentare[index];
            }
            set
            {
                InformatiiSuplimentare[index] = value;
            }
        }
    }
}
