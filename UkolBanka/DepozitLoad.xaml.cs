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
        public DepozitLoad(string nameFile)
        {
            InitializeComponent();
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
                            i++;
                        }
                        else if (i == 1)
                        {
                            actualStateOfMoney.Content = "Aktuální stav - " + line;
                            i++;
                        }
                        else if (i == 2)
                        {
                            actualMaxWithrawl.Content = "Student, max. výběr - " + line;
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
    }
}
