using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace UkolBanka
{
    /// <summary>
    /// Interaction logic for KreditLoad.xaml
    /// </summary>
    public partial class KreditLoad : Window
    {
        string nazev = "";
        bool helpBoolWarning = false;
        int i = 0;
        Kreditnics kreditnics = new Kreditnics();
        DateTime dateTime = new DateTime();
        public KreditLoad(string nameFile)
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
                            kreditnics.NazevUctu = line;
                            i++;
                            nameOfAcc.Content += " - " + line;
                        }
                        else if (i == 1)
                        {
                            kreditnics.Kredit = Convert.ToDouble(line);
                            i++;
                        }
                        else if (i == 2)
                        {
                            kreditnics.ActualSpend = Convert.ToDouble(line);
                            i++;
                        }
                        else if (i == 3)
                        {
                            kreditnics.Warning = Convert.ToBoolean(line);
                            helpBoolWarning = kreditnics.Warning;
                            i++;
                        }
                        else
                        {
                            dateTime = Convert.ToDateTime(line);
                        }
                    }
                }
                actualStateOfMoney.Content = "Aktuální útrata je " + kreditnics.ActualSpend + "Kč a můžete jít maximálně do výše " + kreditnics.Kredit + "Kč.";
                if (kreditnics.Warning == true)
                {
                    pasteMoney.Visibility = Visibility.Visible;
                    pasteMoneyBtn.Visibility = Visibility.Visible;
                    lblPasteMoney.Visibility = Visibility.Visible;
                    DoubleAnimation ell = new DoubleAnimation(1, 80, new Duration(new TimeSpan(8000000)));
                    warningEll.BeginAnimation(HeightProperty, ell);
                    warningEll.BeginAnimation(WidthProperty, ell);
                    DoubleAnimation mat = new DoubleAnimation(1, 50, new Duration(new TimeSpan(8000000)));
                    warningIcon.BeginAnimation(HeightProperty, mat);
                    warningIcon.BeginAnimation(WidthProperty, mat);
                    TimeSpan span = DateTime.Now - dateTime;
                    int day = 45 - span.Days;
                    if (day == 0)
                    {
                        warningEll.ToolTip = "Bohužel dostáváte sankci.. Do zaplacení zbývá 45 dní.";
                        infoUrokBorder.Visibility = Visibility.Visible;
                        Stream stream = new FileStream(nazev, FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(kreditnics.NazevUctu);
                            sw.WriteLine(kreditnics.Kredit);
                            sw.WriteLine(kreditnics.ActualSpend);
                            sw.WriteLine(kreditnics.Warning.ToString());
                            sw.WriteLine(DateTime.Now.ToShortDateString());
                        }
                    }
                    else
                    {
                        warningEll.ToolTip = "Do zaplacení bez sankcí zbývá " + day + "dní.";
                        infoUrokBorder.Visibility = Visibility.Visible;
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

        private void unPasteMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (unPasteMoney.Text != null)
            {
                try
                {
                    kreditnics.ActualSpend += kreditnics.TakeOutMoney(Convert.ToDouble(unPasteMoney.Text));
                    Stream stream = new FileStream(nazev, FileMode.Create);
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(kreditnics.NazevUctu);
                        sw.WriteLine(kreditnics.Kredit);
                        sw.WriteLine(kreditnics.ActualSpend);
                        sw.WriteLine(kreditnics.Warning.ToString());
                        if (helpBoolWarning == true)
                            sw.WriteLine(dateTime.ToShortDateString());
                        else
                            sw.WriteLine(DateTime.Now.ToShortDateString());
                    }
                    actualStateOfMoney.Content = "Aktuální útrata je " + kreditnics.ActualSpend + "Kč a můžete jít maximálně do výše " + kreditnics.Kredit + "Kč.";
                    if (kreditnics.Warning == true)
                    {
                        pasteMoney.Visibility = Visibility.Visible;
                        pasteMoneyBtn.Visibility = Visibility.Visible;
                        lblPasteMoney.Visibility = Visibility.Visible;
                        DoubleAnimation ell = new DoubleAnimation(1, 80, new Duration(new TimeSpan(8000000)));
                        warningEll.BeginAnimation(HeightProperty, ell);
                        warningEll.BeginAnimation(WidthProperty, ell);
                        DoubleAnimation mat = new DoubleAnimation(1, 50, new Duration(new TimeSpan(8000000)));
                        warningIcon.BeginAnimation(HeightProperty, mat);
                        warningIcon.BeginAnimation(WidthProperty, mat);
                        TimeSpan span = DateTime.Now - dateTime;
                        int day = 45 - span.Days;
                        warningEll.ToolTip = "Do zaplacení bez sankcí zbývá " + day + "dní.";
                        infoUrokBorder.Visibility = Visibility.Visible;
                    }
                    unPasteMoney.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Zadat číslo.");
                }
            }
            else
                MessageBox.Show("Vyplnit pole prosim.");
        }

        private void infoBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(kreditnics.ToString(), "Info about " + kreditnics.NazevUctu, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void pasteMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (pasteMoney.Text != null)
            {
                try
                {
                    double s = kreditnics.TakeInMoney(Convert.ToInt32(pasteMoney.Text));
                    if (s == kreditnics.ActualSpend)
                        MessageBox.Show("Prosím nepřeplacovat!");
                    else
                    {
                        kreditnics.ActualSpend = kreditnics.TakeInMoney(Convert.ToInt32(pasteMoney.Text));
                        Console.WriteLine(kreditnics.Warning);
                        Stream stream = new FileStream(nazev, FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(kreditnics.NazevUctu);
                            sw.WriteLine(kreditnics.Kredit);
                            sw.WriteLine(kreditnics.ActualSpend);
                            sw.WriteLine(kreditnics.Warning.ToString());
                            if (kreditnics.Warning == false)
                                sw.WriteLine(dateTime.ToShortDateString());
                            else
                                sw.WriteLine(DateTime.Now.ToShortDateString());
                        }
                        actualStateOfMoney.Content = "Aktuální útrata je " + kreditnics.ActualSpend + "Kč " + "(-" + pasteMoney.Text + "Kč)" + " a můžete jít maximálně do výše " + kreditnics.Kredit + "Kč.";
                        if (kreditnics.Warning == true)
                        {
                            pasteMoney.Visibility = Visibility.Visible;
                            pasteMoneyBtn.Visibility = Visibility.Visible;
                            lblPasteMoney.Visibility = Visibility.Visible;
                            DoubleAnimation ell = new DoubleAnimation(1, 80, new Duration(new TimeSpan(8000000)));
                            warningEll.BeginAnimation(HeightProperty, ell);
                            warningEll.BeginAnimation(WidthProperty, ell);
                            DoubleAnimation mat = new DoubleAnimation(1, 50, new Duration(new TimeSpan(8000000)));
                            warningIcon.BeginAnimation(HeightProperty, mat);
                            warningIcon.BeginAnimation(WidthProperty, mat);
                            TimeSpan span = DateTime.Now - dateTime;
                            int day = 45 - span.Days;
                            warningEll.ToolTip = "Do zaplacení bez sankcí zbývá " + day + "dní.";
                            infoUrokBorder.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            pasteMoney.Visibility = Visibility.Hidden;
                            pasteMoneyBtn.Visibility = Visibility.Hidden;
                            lblPasteMoney.Visibility = Visibility.Hidden;
                            DoubleAnimation ell = new DoubleAnimation(80, 1, new Duration(new TimeSpan(8000000)));
                            warningEll.BeginAnimation(HeightProperty, ell);
                            warningEll.BeginAnimation(WidthProperty, ell);
                            DoubleAnimation mat = new DoubleAnimation(50, 1, new Duration(new TimeSpan(8000000)));
                            warningIcon.BeginAnimation(HeightProperty, mat);
                            warningIcon.BeginAnimation(WidthProperty, mat);
                            infoUrokBorder.Visibility = Visibility.Hidden;
                        }
                        pasteMoney.Text = "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Zadat číslo.");
                }
            }
            else
                MessageBox.Show("Vyplnit pole prosim.");
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            virtaulDate.Content = calendar.SelectedDate.Value.ToString();
            txtPredikce.Text = kreditnics.CalculateSpendings(calendar.SelectedDate.Value);
        }
    }
}
