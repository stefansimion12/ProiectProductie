using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    internal class Produs : ICloneable, IConversieEuro
    {
        private string nume;
        private int cantitate;
        private double pret;

        public Produs(){ }
        public Produs(string nume, int cantitate, double pret)
        {
            this.nume = nume;
            this.cantitate = cantitate;
            this.pret = pret;
        }

        public string Nume { get => nume; set => nume = value; }
        public int Cantitate { get => cantitate; set => cantitate = value; }
        public double Pret { get => pret; set => pret = value; }

        public static Produs operator+(Produs p, double pret)
        {
            if(p != null)
            {
                p.pret += pret;
            }
            return p;
        }

        public static bool operator >(Produs p1, Produs p2)
        {
            if(p1 != null && p2 != null)
            {
                if (p1.pret * p1.Cantitate > p2.pret * p2.Cantitate)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool operator <(Produs p1, Produs p2)
        {
            if (p1 != null && p2 != null)
            {
                if (p1.pret * p1.Cantitate < p2.pret * p2.Cantitate)
                {
                    return true;
                }
            }
            return false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public double ConversieEuro()
        {
            return this.pret / 4.97;
        }
    }
}
