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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UkolBanka
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckForAccountsToLoad();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            DepozitWindow depozitWindow = new DepozitWindow();
            depozitWindow.ShowDialog();
            CheckForAccountsToLoad();
        }

        private void Kredit_Click(object sender, RoutedEventArgs e)
        {
            KreditWindow kreditWindow = new KreditWindow();
            kreditWindow.ShowDialog();
            CheckForAccountsToLoad();
        }

        private void CreateAcc_Click(object sender, RoutedEventArgs e)
        {
            if (lblAccounts.SelectedItem != null)
            {
                DepozitLoad depozitLoad = new DepozitLoad(lblAccounts.SelectedItem.ToString() + ".txt");
                depozitLoad.ShowDialog();
            }
            else if (lblAccKredit.SelectedItem != null)
            {
                KreditLoad kreditLoad = new KreditLoad(lblAccKredit.SelectedItem.ToString() + ".krd");
                kreditLoad.ShowDialog();
            }
            lblAccounts.SelectedItem = null;
            lblAccKredit.SelectedItem = null;
        }

        public void CheckForAccountsToLoad()
        {
            lblAccounts.Items.Clear();
            lblAccKredit.Items.Clear();
            if (File.Exists("accounts.txt"))
            {
                using (StreamReader sr = new StreamReader("accounts.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lblAccounts.Items.Add(line);
                    }
                }
            }
            if (File.Exists("accountsKredit.txt"))
            {
                using (StreamReader sr = new StreamReader("accountsKredit.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lblAccKredit.Items.Add(line);
                    }
                }
            }
            lblAccounts.SelectedItem = null;
            lblAccKredit.SelectedItem = null;
        }
    }
}
