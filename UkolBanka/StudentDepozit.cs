using System;
using System.Collections.Generic;
using System.Globalization;
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
        public StudentDepozit() {}
        public override string ToString()
        {
            return base.ToString() + " A jelikož je studentský, tak má maximální výběr " + MaxVyber + "Kč s 1,6% zúročněním za rok.";
        }
        public override double MinVklad(double vkl)
        {
            if (vkl > MaxVyber)
            {
                return Vklad;
            }
            return base.MinVklad(vkl);
        }
    }
}
