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
            return "Kreditní účet s názvem " + NazevUctu + " má stav kreditu -" + ActualSpend + "Kč z max částky " + Kredit + "Kč s 3,6% úrokem";
        }
        public double TakeOutMoney(double money)
        {
            if (money > Kredit || (money + ActualSpend) > Kredit)
                Warning = true;
            return Kredit - (Kredit - money);
        }
        public double TakeInMoney(double money)
        {
            if (money < ActualSpend)
            {
                return ActualSpend - money;
            }
            else if (money == ActualSpend)
            {
                Warning = false;
                return ActualSpend - money;
            }
            return ActualSpend;
        }
        public string CalculateSpendings(DateTime virtualDate)
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
            if (vYear < nYear)
            {
                return "Musíte jít do budoucnosti!";
            }
            else if (vYear > nYear)
            {
                if (vDay < nDay)
                {
                    if (i > 1)
                    {
                        newCastka = ActualSpend * (3.6 / (12 - (12 - dMonth + vMonth)) * z) / 100 + ActualSpend;
                    }
                    else
                        return "Částky se načítají až po měsíci.";
                }
                else
                {
                    newCastka = ActualSpend * (3.6 / (12 - (12 - dMonth + vMonth)) * z) / 100 + ActualSpend;
                }
            }
            else if (vYear == nYear)
            {
                if (vMonth <= dMonth)
                {
                    return "Musíte jít do budoucnosti!";
                }
                else
                {
                    if (vMonth > dMonth && vDay < nDay)
                    {
                        newCastka = ActualSpend * (3.6 / (12 - i)) / 100 + ActualSpend;
                    }
                    else if (vDay < nDay)
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
                                newCastka = ActualSpend * (3.6 / (12 - i)) / 100 + ActualSpend;
                            }
                        }
                    }
                }
            }

            return "Při aktuální útratě " + ActualSpend + "Kč by se Váš učet navýšil k " + virtualDate.ToShortDateString() + " na částku " + newCastka.ToString() + "Kč.";

        }
    }
}
