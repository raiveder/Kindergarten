using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для ViewEmployees.xaml
    /// </summary>
    public partial class ViewEmployees : Page
    {
        public ViewEmployees()
        {
            InitializeComponent();
            dg.ItemsSource = Base.KE.Employees.ToList();
            tboxFind.Visibility = Visibility.Collapsed;
        }

        private void rbSortAsc_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = Base.KE.Employees.OrderBy(x => x.Surname).ToList();
        }

        private void rbSortDesc_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = Base.KE.Employees.OrderByDescending(x => x.Surname).ToList();
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)
        {
            rbSortAsc.IsChecked = false;
            rbSortDesc.IsChecked = false;
            rbMale.IsChecked = false;
            rbFemale.IsChecked = false;
            rbFindSurname.IsChecked = false;
            rbFindName.IsChecked = false;
            tboxFind.Visibility = Visibility.Collapsed;
            dg.ItemsSource = Base.KE.Employees.ToList();
        }

        private void rbMale_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = Base.KE.Employees.Where(x => x.Genders.Id_gender == 1).ToList();
        }

        private void rbFemale_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = Base.KE.Employees.Where(x => x.Genders.Id_gender == 2).ToList();
        }

        private void rbFindSurname_Click(object sender, RoutedEventArgs e)
        {
            tboxFind.Visibility = Visibility.Visible;
        }

        private void tboxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Employees> list = new List<Employees>();
            if (rbFindSurname.IsChecked == true)
            {
                foreach (Employees emp in Base.KE.Employees)
                {
                    if (emp.Surname.Substring(0, tboxFind.Text.Length) == tboxFind.Text)
                    {
                        list.Add(emp);
                    }
                }
            }
            else
            {
                foreach (Employees emp in Base.KE.Employees)
                {
                    if (emp.Names.Substring(0, tboxFind.Text.Length) == tboxFind.Text)
                    {
                        list.Add(emp);
                    }
                }
            }
            dg.ItemsSource = list;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new AdminMenu());
        }
    }
}