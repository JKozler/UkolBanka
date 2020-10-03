﻿using System;
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
        public StudentDepozit() {}
        public override string ToString()
        {
            return base.ToString() + " A jelikož je studentský, tak má maximální výběr " + MaxVyber + "Kč s 1,2% zúročněním za rok.";
        }
    }
}
