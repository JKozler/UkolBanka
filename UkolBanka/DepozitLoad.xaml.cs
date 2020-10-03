﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UkolBanka
{
    /// <summary>
    /// Interaction logic for DepozitLoad.xaml
    /// </summary>
    public partial class DepozitLoad : Window
    {
        int i = 0;
        bool student = false;
        string nazev = "";
        Depozitnics depozitnics = new Depozitnics();
        StudentDepozit studentDepozit = new StudentDepozit();
        public DepozitLoad(string nameFile)
        {
            InitializeComponent();
            nazev = nameFile;
            actualDate.Content = DateTime.Now.ToShortDateString();
            if (File.Exists(nameFile))
            {
                using (StreamReader sr = new StreamReader(nameFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (i == 0)
                        {
                            nameOfAcc.Content = "Depozitní účet - " + line;
                            if (nameFile.Contains("student"))
                            {
                                studentDepozit.NazevUctu = line;
                                student = true;
                            }
                            else
                                depozitnics.NazevUctu = line;
                            i++;
                        }
                        else if (i == 1)
                        {
                            actualStateOfMoney.Content = "Aktuální stav - " + line + "Kč.";
                            if (nameFile.Contains("student"))
                                studentDepozit.Vklad = Convert.ToDouble(line);
                            else
                                depozitnics.Vklad = Convert.ToDouble(line);
                            i++;
                        }
                        else if (i == 2)
                        {
                            actualMaxWithrawl.Content = "Student, max. výběr - " + line + "Kč.";
                            studentDepozit.MaxVyber = Convert.ToDouble(line);
                            i++;
                        }
                    }
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pasteMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(nazev))
            {
                if (pasteMoney.Text != null)
                {
                    if (student)
                    {
                        studentDepozit.Vklad += Convert.ToDouble(pasteMoney.Text);
                        Stream stream = new FileStream(nazev, FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(studentDepozit.NazevUctu);
                            sw.WriteLine(studentDepozit.Vklad);
                            sw.WriteLine(studentDepozit.MaxVyber);
                        }
                        actualStateOfMoney.Content = "Aktuální stav - " + studentDepozit.Vklad.ToString();
                    }
                    else
                    {
                        depozitnics.Vklad += Convert.ToDouble(pasteMoney.Text);
                        Stream stream = new FileStream(nazev, FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(depozitnics.NazevUctu);
                            sw.WriteLine(depozitnics.Vklad);
                        }
                        actualStateOfMoney.Content = "Aktuální stav - " + depozitnics.Vklad.ToString();
                    }
                    pasteMoney.Clear();
                }
            }
        }

        private void unPasteMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(nazev))
            {
                if (unPasteMoney.Text != null)
                {
                    if (student)
                    {
                        if (Convert.ToDouble(unPasteMoney.Text) <= studentDepozit.MaxVyber)
                        {
                            if ((studentDepozit.Vklad - Convert.ToDouble(unPasteMoney.Text)) >= 0)
                            {
                                studentDepozit.Vklad -= Convert.ToDouble(unPasteMoney.Text);
                                Stream stream = new FileStream(nazev, FileMode.Create);
                                using (StreamWriter sw = new StreamWriter(stream))
                                {
                                    sw.WriteLine(studentDepozit.NazevUctu);
                                    sw.WriteLine(studentDepozit.Vklad);
                                    sw.WriteLine(studentDepozit.MaxVyber);
                                }
                                actualStateOfMoney.Content = "Aktuální stav - " + studentDepozit.Vklad.ToString();
                            }
                            else
                                MessageBox.Show("You can't under 0.");
                        }
                        else
                            MessageBox.Show("You can't go under your max payout.");
                    }
                    else
                    {
                        if ((depozitnics.Vklad - Convert.ToDouble(unPasteMoney.Text)) >= 0)
                        {
                            depozitnics.Vklad -= Convert.ToDouble(unPasteMoney.Text);
                            Stream stream = new FileStream(nazev, FileMode.Create);
                            using (StreamWriter sw = new StreamWriter(stream))
                            {
                                sw.WriteLine(depozitnics.NazevUctu);
                                sw.WriteLine(depozitnics.Vklad);
                            }
                            actualStateOfMoney.Content = "Aktuální stav - " + depozitnics.Vklad.ToString();
                        }
                        else
                            MessageBox.Show("You can't under 0.");
                    }
                    unPasteMoney.Clear();
                }
            }
        }

        private void infoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (student)
                MessageBox.Show(studentDepozit.ToString(), "Info about " + studentDepozit.NazevUctu, MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(depozitnics.ToString(), "Info about " + depozitnics.NazevUctu, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            virtaulDate.Content = calendar.SelectedDate.Value.ToString();
            txtPredikce.Text = depozitnics.CalculateEarnings(calendar.SelectedDate.Value);
        }
    }
}
