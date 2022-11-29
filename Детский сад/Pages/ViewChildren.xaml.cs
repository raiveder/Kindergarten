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
        Pagination Pagin = new Pagination();
        List<Children> ChildList = new List<Children>();

        public ViewChildren()
        {
            InitializeComponent();
            CbFilter.SelectedIndex = 0;
            ChildList = Base.KE.Children.ToList();
            Pagin.CountPage = Base.KE.Children.ToList().Count;
            DataContext = Pagin;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new Account());
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

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void TBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void ChBPhoto_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            List<Children> list = new List<Children>();

            switch (CbFilter.SelectedIndex)
            {
                case 1:
                    list = Base.KE.Children.Where(x => x.Surname.StartsWith(TBoxFind.Text)).ToList();
                    break;
                case 2:
                    list = Base.KE.Children.Where(x => x.Names.StartsWith(TBoxFind.Text)).ToList();
                    break;
                case 3:
                    list = Base.KE.Children.Where(x => x.Patronymic.StartsWith(TBoxFind.Text)).ToList();
                    break;
                case 4:
                    foreach (Children item in Base.KE.Children)
                    {
                        if (item.Groups.Name_group.StartsWith(TBoxFind.Text))
                        {
                            list.Add(item);
                        }
                    }

                    break;
                default:
                    list = Base.KE.Children.ToList();
                    break;
            }

            if ((bool)ChBPhoto.IsChecked)
            {
                list = list.Where(x => x.Photo != "\\Resources\\Заглушка.png").ToList();
            }

            switch (CbSort.SelectedIndex)
            {
                case 1:
                    list.Sort((x, y) => x.Surname.CompareTo(y.Surname));
                    break;
                case 2:
                    list.Sort((x, y) => x.Names.CompareTo(y.Names));
                    break;
                case 3:
                    list.Sort((x, y) => x.Patronymic.CompareTo(y.Patronymic));
                    break;
                case 4:
                    list.Sort((x, y) => x.Surname.CompareTo(y.Surname));
                    list.Reverse();
                    break;
                case 5:
                    list.Sort((x, y) => x.Names.CompareTo(y.Names));
                    list.Reverse();
                    break;
                case 6:
                    list.Sort((x, y) => x.Patronymic.CompareTo(y.Patronymic));
                    list.Reverse();
                    break;
            }

            lv.ItemsSource = list;
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)
            {
                case "back":
                    Pagin.CurrentPage--;
                    break;
                case "next":
                    Pagin.CurrentPage++;
                    break;
                default:
                    Pagin.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }

            lv.ItemsSource = ChildList.Skip(Pagin.CurrentPage * Pagin.CountPage - Pagin.CountPage).Take(Pagin.CountPage).ToList();
        }

        private void tboxPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Pagin.CountPage = Convert.ToInt32(tboxPageCount.Text);
            }
            catch
            {
                Pagin.CountPage = ChildList.Count;
            }

            Pagin.CountList = ChildList.Count;
            lv.ItemsSource = ChildList.Skip(0).Take(Pagin.CountPage).ToList();
            Pagin.CurrentPage = 1;
        }
    }
}