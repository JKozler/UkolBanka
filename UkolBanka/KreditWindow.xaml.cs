using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UkolBanka
{
    /// <summary>
    /// Interaction logic for KreditWindow.xaml
    /// </summary>
    public partial class KreditWindow : Window
    {
        Kreditnics kreditnics = new Kreditnics();
        public KreditWindow()
        {
            InitializeComponent();
            actualDate.Content = DateTime.Now.ToShortDateString();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (mzdaTxt.Text != null && accName.Text != null)
            {
                try
                {
                    kreditnics.NazevUctu = accName.Text;
                    kreditnics.Kredit = Kreditnics.CalculateSpendLevel(Convert.ToInt32(mzdaTxt.Text));
                    kreditnics.ActualSpend = 0.0;
                    kreditnics.Warning = false;
                    Stream stream = new FileStream("accountsKredit.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(kreditnics.NazevUctu);
                    }
                    Stream stream1 = new FileStream(kreditnics.NazevUctu + ".krd", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream1))
                    {
                        sw.WriteLine(kreditnics.NazevUctu);
                        sw.WriteLine(kreditnics.Kredit);
                        sw.WriteLine(kreditnics.ActualSpend);
                        sw.WriteLine(kreditnics.Warning.ToString());
                        sw.WriteLine(DateTime.Now.ToShortDateString());
                    }

                    DoubleAnimation ell = new DoubleAnimation(1, 100, new Duration(new TimeSpan(8000000)));
                    doneellipse.BeginAnimation(HeightProperty, ell);
                    doneellipse.BeginAnimation(WidthProperty, ell);
                    DoubleAnimation mat = new DoubleAnimation(1, 40, new Duration(new TimeSpan(8000000)));
                    doneMaterial.BeginAnimation(HeightProperty, mat);
                    doneMaterial.BeginAnimation(WidthProperty, mat);
                    closeIt.Content = "Můžete zavřít, váš účet má limit pro útratu " + kreditnics.Kredit + "Kč";
                    closeIt.Visibility = Visibility.Visible;
                }
                catch (Exception)
                {
                    MessageBox.Show("Zadat číslo.");
                }
            }
            else
                MessageBox.Show("Musite vzplnit vsechna pole.");
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
