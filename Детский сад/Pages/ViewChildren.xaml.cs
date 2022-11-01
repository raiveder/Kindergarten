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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для ViewChildren.xaml
    /// </summary>
    public partial class ViewChildren : Page
    {
        public ViewChildren()
        {
            InitializeComponent();
            lv.ItemsSource = Base.KE.Children.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new AdminMenu());
        }

        private void tbMather_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int id = Convert.ToInt32(tb.Uid);

            Kinships kinships = Base.KE.Kinships.FirstOrDefault(x => x.Id_child == id && x.Parents.Id_gender == 2);

            if (kinships != null)
            {
                tb.Text = "Мать: ";
                tb.Text += kinships.Parents.FullName;
            }
            else
            {
                tb.Visibility = Visibility.Collapsed;
            }
        }

        private void tbVather_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int id = Convert.ToInt32(tb.Uid);

            Kinships kinships = Base.KE.Kinships.FirstOrDefault(x => x.Id_child == id && x.Parents.Id_gender == 1);

            if (kinships != null)
            {
                tb.Text = "Отец: ";
                tb.Text += kinships.Parents.FullName;
            }
            else
            {
                tb.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new AddChild());
        }
    }
}