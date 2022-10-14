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
            dgv.ItemsSource = Base.KE.Employees.ToList();
        }
        private void btnSortAsc_Click(object sender, RoutedEventArgs e)
        {
            dgv.ItemsSource = Base.KE.Employees.ToList().OrderBy(x=>x.Surname).ToList();
        }

        private void btnSortDesc_Click(object sender, RoutedEventArgs e)
        {
            dgv.ItemsSource = Base.KE.Employees.ToList().OrderByDescending(x => x.Surname).ToList();
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)
        {
            dgv.ItemsSource = Base.KE.Employees.ToList();
        }
    }
}
