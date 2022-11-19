using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            Base.mainFrame.Navigate(new Account(Base.User));
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Children child = Base.KE.Children.FirstOrDefault(x => x.Id_child == id);
            List<Kinships> kinships = Base.KE.Kinships.Where(x => x.Id_child == id).ToList();

            Base.KE.Children.Remove(child);
            Base.KE.SaveChanges();

            foreach (Kinships item in kinships)
            {
                if (Base.KE.Kinships.Where(x => x.Id_parent == item.Id_parent).ToList().Count == 0)
                {
                    Parents parent = Base.KE.Parents.FirstOrDefault(x => x.Id_parent == item.Id_parent);
                    Base.KE.Parents.Remove(parent);
                }
            }

            Base.KE.SaveChanges();
            MessageBox.Show("Ребёнок успешно удалён", "Дети", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void lv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Children child = (Children)lv.SelectedItem;
            Base.mainFrame.Navigate(new AddChild(Base.KE.Children.FirstOrDefault(x => x.Id_child == child.Id_child)));
        }
    }
}