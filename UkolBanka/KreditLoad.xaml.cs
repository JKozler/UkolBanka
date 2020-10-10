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
                    warningEll.ToolTip = "Do zaplacení bez sankcí zbývá " + day + "dní.";
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
                    }
                    unPasteMoney.Text = "";
                }
                catch (Exception)
                {
                    throw new Exception("Cislo zadat");
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

        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
