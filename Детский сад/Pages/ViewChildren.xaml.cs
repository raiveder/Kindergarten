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

        private void tbParents_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int id = Convert.ToInt32(tb.Uid);

            List<Kinships> list = Base.KE.Kinships.Where(x => x.Id_child == id).ToList();

            string parents;
            if (list.Count == 1)
            {
               parents = "Родитель: ";
            }
            else
            {
                parents = "Родители: ";
            }
            for (int i = 0; i < list.Count; i++)
            {
                parents += list[i].Parents.FullName;
                parents += ", ";
            }

            parents = parents.Substring(0, parents.Length - 2);

            tb.Text = parents;
        }
    }
}
