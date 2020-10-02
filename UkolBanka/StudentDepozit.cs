using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkolBanka
{
    class StudentDepozit : Depozitnics
    {
        public double MaxVyber { get; set; }
        public StudentDepozit(double maxVyber, string nazevUctu, double vklad) : base (nazevUctu, vklad)
        {
            MaxVyber = maxVyber;
        }
    }
}
