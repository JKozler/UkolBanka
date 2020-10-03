using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkolBanka
{
    class Depozitnics : IUzivatel
    {
        public string NazevUctu { get; set; }
        public double Vklad { get; set; }
        public Depozitnics(string nazevUctu, double vklad)
        {
            NazevUctu = nazevUctu;
            Vklad = vklad;
        }
        public Depozitnics() {}
        public virtual string ToString()
        {
            return "Účet s názvem " + NazevUctu + " má stav účtu " + Vklad + "Kč.";
        }
    }
}
