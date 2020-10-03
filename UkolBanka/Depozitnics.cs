using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UkolBanka
{
    class Depozitnics : IUzivatel
    {
        public string NazevUctu { get; set; }
        private double myVklad;

        public double Vklad
        {
            get { return myVklad; }
            set { myVklad = value; }
        }

        public Depozitnics(string nazevUctu, double vklad)
        {
            NazevUctu = nazevUctu;
            Vklad = vklad;
        }
        public Depozitnics() { }
        public virtual string ToString()
        {
            return "Účet s názvem " + NazevUctu + " má stav účtu " + Vklad + "Kč s 1,6% zúročněním za rok.";
        }
        public string CalculateEarnings(DateTime virtualDate)
        {
            int vDay = virtualDate.Day;
            int nDay = DateTime.Now.Day;
            int vMonth = virtualDate.Month;
            int dMonth = DateTime.Now.Month;
            int vYear = virtualDate.Year;
            int nYear = DateTime.Now.Year;
            int i = vMonth - dMonth;
            int y = vDay - nDay;
            int z = vYear - nYear;
            double newCastka = 0;
            if (vYear == nYear)
            {
                if (vMonth <= dMonth)
                {
                    return "Musíte jít do budoucnosti!";
                }
                else
                {
                    if (vDay < nDay)
                    {
                        return "Částky se načítají až po měsíci.";
                    }
                    else
                    {
                        if (i == 1 && vDay < nDay)
                        {
                            return "Částky se načítají až po měsíci.";
                        }
                        else
                        {
                            if (y >= 0)
                            {
                                newCastka = Vklad * (1.6 / (12 - i)) / 100 + Vklad;
                            }
                        }
                    }
                }
            }
            else if (vYear < nYear)
            {
                return "Musíte jít do budoucnosti!";
            }
            else
            {
                if (vDay < nDay)
                {
                    if (i > 1)
                    {
                        newCastka = Vklad * (1.6 / (12 - (12 - dMonth + vMonth)) * z) / 100 + Vklad;
                    }
                    else
                        return "Částky se načítají až po měsíci.";
                }
                else
                {
                    newCastka = Vklad * (1.6 / (12 - (12 - dMonth + vMonth)) * z) / 100 + Vklad;
                }
            }
            
            return "Při aktuálním pozůstatku " + Vklad + "Kč by se Váš učet navýšil k " + virtualDate.ToShortDateString() + " na částku " + newCastka.ToString() + "Kč.";
        }
    }
}
