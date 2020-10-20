using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UkolBanka
{
    public class Depozitnics : IUzivatel
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
        public double AddVklad(double vkl)
        {
            return Vklad + vkl;
        }
        public virtual double MinVklad(double vkl)
        {
            if (Vklad < vkl)
            {
                return Vklad;
            }
            return Vklad - vkl;
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
            if (Vklad > 100000.00)
            {
                if (vYear == nYear)
                {
                    if (vMonth <= dMonth)
                    {
                        return "Musíte jít do budoucnosti!";
                    }
                    else
                    {
                        if (vMonth > dMonth && vDay < nDay)
                        {
                            newCastka = Vklad * (1.1 / (12 - i)) / 100 + Vklad;
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
                                    newCastka = Vklad * (1.1 / (12 - i)) / 100 + Vklad;
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
                            double helpHolderMoney = Vklad * (1.1 / (12 - dMonth)) / 100;
                            newCastka = Vklad * ((1.1 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                        }
                        else if (vMonth == 1 && dMonth == 12 && vDay < nDay)
                            return "Částky se načítají až po měsíci.";
                        else
                        {
                            double helpHolderMoney = Vklad * (1.6 / (12 - dMonth)) / 100;
                            newCastka = Vklad * ((1.6 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                        }
                    }
                    else
                    {
                        double helpHolderMoney = Vklad * (1.1 / (12 - dMonth)) / 100;
                        newCastka = Vklad * ((1.1 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                    }
                }
            }
            else
            {
                if (vYear == nYear)
                {
                    if (vMonth <= dMonth)
                    {
                        return "Musíte jít do budoucnosti!";
                    }
                    else
                    {
                        if (vMonth > dMonth && vDay < nDay)
                        {
                            newCastka = Vklad * (1.6 / (12 - i)) / 100 + Vklad;
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
                        if (z >= 2)
                        {
                            double helpHolderMoney = Vklad * (1.6 / (12 - dMonth)) / 100;
                            newCastka = Vklad * ((1.6 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                        }
                        else
                        {
                            if (i > 1)
                            {
                                double helpHolderMoney = Vklad * (1.6 / (12 - dMonth)) / 100;
                                newCastka = Vklad * ((1.6 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                            }
                            else if (vMonth == 1 && dMonth == 12 && vDay < nDay)
                                return "Částky se načítají až po měsíci.";
                            else
                            {
                                double helpHolderMoney = Vklad * (1.6 / (12 - dMonth)) / 100;
                                newCastka = Vklad * ((1.6 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                            }
                        }
                    }
                    else
                    {
                        double helpHolderMoney = Vklad * (1.6 / (12 - dMonth)) / 100;
                        newCastka = Vklad * ((1.6 / (12 - vMonth)) * z) / 100 + Vklad + helpHolderMoney;
                    }
                }
            }
            
            return "Při aktuálním pozůstatku " + Vklad + "Kč by se Váš učet navýšil k " + virtualDate.ToShortDateString() + " na částku " + newCastka.ToString() + "Kč.";
        }
    }
}
