using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkolBanka
{
    class Kreditnics : IUzivatel
    {
        public string NazevUctu { get; set; }
        public double ActualSpend { get; set; }
        public bool Warning { get; set; }
        private double myKredit;

        public double Kredit
        {
            get { return myKredit; }
            set { myKredit = value; }
        }

        public Kreditnics(string nazevUctu, double kredit, double actualSpend, bool warning)
        {
            NazevUctu = nazevUctu;
            Kredit = kredit;
            ActualSpend = actualSpend;
            Warning = warning;
        }
        public Kreditnics(){}
        public static double CalculateSpendLevel(double money)
        {
            return money - (money * 0.2);
        }
        public override string ToString()
        {
            return "Kreditní účet s názvem " + NazevUctu + " má stav kreditu " + Kredit + "Kč s 3,6% úrokem";
        }
        public double TakeOutMoney(double money)
        {
            if (money > Kredit || (money + ActualSpend) > Kredit)
                Warning = true;
            return Kredit - (Kredit - money);
        }
        public string CalculateSpendings(DateTime virtualDate, double vybranaCastka)
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
            double newCastka = myKredit;

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
                        newCastka = myKredit - vybranaCastka * 3.6;
                    }

                }
            }
            if (newCastka >= -10000)
            {
                return "Při výběru " + vybranaCastka + "Kč by se Váš kredit snížil " + virtualDate.ToShortDateString() + " na částku " + newCastka.ToString() + "Kč.";
            }
            else
            {
                return "Limit vašeho kreditu je -10 000 Kč.";
            }

        }
    }
}
