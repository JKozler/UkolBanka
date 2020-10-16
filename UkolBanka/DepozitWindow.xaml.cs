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
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UkolBanka
{
    /// <summary>
    /// Interakční logika pro DepozitWindow.xaml
    /// </summary>
    public partial class DepozitWindow : Window
    {
        bool student = false;
        public DepozitWindow()
        {
            InitializeComponent();
            actualDate.Content = DateTime.Now.ToShortDateString();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void studentDepzit_Click(object sender, RoutedEventArgs e)
        {
            if (student)
            {
                maxVzbratStudent.Visibility = Visibility.Hidden;
                studentDepzit.Background = new SolidColorBrush(Colors.PaleVioletRed);
                maxTxt.Visibility = Visibility.Hidden;
                student = false;
            }
            else
            {
                maxVzbratStudent.Visibility = Visibility.Visible;
                studentDepzit.Background = new SolidColorBrush(Colors.LightGreen);
                maxTxt.Visibility = Visibility.Visible;
                student = true;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(vkladTxt.Text) >= 200)
            {
                if (student == false)
                {
                    if (vkladTxt.Text != null && nameAcc.Text != null)
                    {
                        Depozitnics depozitnics = new Depozitnics(nameAcc.Text, Convert.ToDouble(vkladTxt.Text));
                        Stream stream = new FileStream("accounts.txt", FileMode.Append);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(depozitnics.NazevUctu);
                        }
                        Stream stream2 = new FileStream(depozitnics.NazevUctu + ".txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream2))
                        {
                            sw.WriteLine(depozitnics.NazevUctu);
                            sw.WriteLine(depozitnics.Vklad);
                            sw.WriteLine(DateTime.Now.ToShortDateString());
                        }
                        DoubleAnimation ell = new DoubleAnimation(1, 100, new Duration(new TimeSpan(8000000)));
                        doneellipse.BeginAnimation(HeightProperty, ell);
                        doneellipse.BeginAnimation(WidthProperty, ell);
                        DoubleAnimation mat = new DoubleAnimation(1, 40, new Duration(new TimeSpan(8000000)));
                        doneMaterial.BeginAnimation(HeightProperty, mat);
                        doneMaterial.BeginAnimation(WidthProperty, mat);
                        closeIt.Visibility = Visibility.Visible;
                        Stream stream3 = new FileStream(depozitnics.NazevUctu + "-transaction.txt", FileMode.Append);
                        using (StreamWriter sw = new StreamWriter(stream3))
                        {
                            sw.WriteLine("Vytvoření účtu " + DateTime.Now.ToShortDateString());
                            sw.WriteLine("Vklad " + vkladTxt.Text + " Kč.");
                        }
                        vkladTxt.Clear();
                        nameAcc.Clear();
                    }
                }
                else
                {
                    if (vkladTxt.Text != null && nameAcc.Text != null && maxTxt.Text != null)
                    {
                        StudentDepozit studentDepozit = new StudentDepozit(Convert.ToDouble(maxTxt.Text), nameAcc.Text, Convert.ToDouble(vkladTxt.Text));
                        Stream stream = new FileStream("accounts.txt", FileMode.Append);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(studentDepozit.NazevUctu + "-student");
                        }
                        Stream stream2 = new FileStream(studentDepozit.NazevUctu + "-student.txt", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream2))
                        {
                            sw.WriteLine(studentDepozit.NazevUctu);
                            sw.WriteLine(studentDepozit.Vklad);
                            sw.WriteLine(DateTime.Now.ToShortDateString());
                            sw.WriteLine(studentDepozit.MaxVyber);
                        }
                        DoubleAnimation ell = new DoubleAnimation(1, 100, new Duration(new TimeSpan(8000000)));
                        doneellipse.BeginAnimation(HeightProperty, ell);
                        doneellipse.BeginAnimation(WidthProperty, ell);
                        DoubleAnimation mat = new DoubleAnimation(1, 40, new Duration(new TimeSpan(8000000)));
                        doneMaterial.BeginAnimation(HeightProperty, mat);
                        doneMaterial.BeginAnimation(WidthProperty, mat);
                        closeIt.Visibility = Visibility.Visible;
                        Stream stream3 = new FileStream(studentDepozit.NazevUctu + "-student-transaction.txt", FileMode.Append);
                        using (StreamWriter sw = new StreamWriter(stream3))
                        {
                            sw.WriteLine("Vytvoření studenstkého účtu " + DateTime.Now.ToShortDateString());
                            sw.WriteLine("Vklad " + vkladTxt.Text + " Kč.");
                        }
                        vkladTxt.Clear();
                        nameAcc.Clear();
                        maxTxt.Clear();
                    }

                }
            }
            else
                MessageBox.Show("Malý vklad!");
        }
    }
}
