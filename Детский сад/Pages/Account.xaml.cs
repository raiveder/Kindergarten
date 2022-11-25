using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        private Employees User;
        private List<Photos> ListPhoto;
        private int IdCurrentPhoto;

        public Account()
        {
            InitializeComponent();

            User = Base.User;
            tbFullName.Text += User.Surname + " " + User.Names + " " + User.Patronymic;
            tbBirthdate.Text += User.Birthdate.ToString("D");
            tbPosition.Text += User.Positions.Position;
            tbAdress.Text += User.Street.ToLower() + ", " + User.Building;
            tbLogin.Text += User.Login_user;
            tbRole.Text += User.Roles.Role_name.ToLower();

            ListPhoto = Base.KE.Photos.Where(x => x.Id_employee == User.Id_employee).ToList();
            IdCurrentPhoto = ListPhoto.Count - 1;
            Photos photo;

            if (IdCurrentPhoto != -1)
            {
                photo = ListPhoto[IdCurrentPhoto];
            }
            else
            {
                photo = null;
            }

            imgPhoto.Source = Images.GetBitmapImage(photo);
            imgPhoto.Stretch = Stretch.Uniform;

            if (User.Id_role == 2)
            {
                List<Distributions> list = Base.KE.Distributions.Where(x => x.Id_employee == User.Id_employee).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    tbGroups.Text += list[i].Groups.Name_group + ", ";
                    if (i % 2 == 1 && i != list.Count - 1)
                    {
                        tbGroups.Text += "\n";
                    }
                }

                tbGroups.Text = tbGroups.Text.Remove(tbGroups.Text.Length - 2, 2);
                tbGroups.Visibility = Visibility.Visible;

                spAdmin.Visibility = Visibility.Collapsed;
                spUser.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                if (Images.AddPhoto(ofd.FileName, User.Id_employee))
                {
                    Base.mainFrame.Navigate(new Account());
                    MessageBox.Show("Фото успешно добавлено", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Возникла ошибка! Обратитесь к администратору", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnDelPhoto_Click(object sender, RoutedEventArgs e)
        {

            if (IdCurrentPhoto == -1)
            {
                MessageBox.Show("Нельзя удалить стандартное изображение!", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Photos photo = ListPhoto[IdCurrentPhoto];

            Base.KE.Photos.Remove(photo);
            Base.KE.SaveChanges();

            ListPhoto = Base.KE.Photos.Where(x => x.Id_employee == User.Id_employee).ToList();

            IdCurrentPhoto--;

            if (IdCurrentPhoto == -1)
            {
                photo = null;
                BtnBackPhoto.Visibility = Visibility.Hidden;
            }
            else
            {
                if (IdCurrentPhoto == 0)
                {
                    BtnBackPhoto.Visibility = Visibility.Hidden;
                }

                photo = ListPhoto[IdCurrentPhoto];
            }
            imgPhoto.Source = Images.GetBitmapImage(photo);
        }

        private void BtnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (BtnChangePhoto.Content.ToString() == "Изменить")
            {
                BtnChangePhoto.Content = "Сохранить";
                if (IdCurrentPhoto != -1)
                {
                    BtnBackPhoto.Visibility = Visibility.Visible;
                }

            }
            else
            {
                BtnChangePhoto.Content = "Изменить";
                BtnBackPhoto.Visibility = Visibility.Hidden;
                BtnNextPhoto.Visibility = Visibility.Hidden;

                if (IdCurrentPhoto != -1)
                {
                    Photos photo = ListPhoto[IdCurrentPhoto];
                    Photos newPhoto = new Photos()
                    {
                        Id_employee = photo.Id_employee,
                        Byte_photo = photo.Byte_photo,
                        Path_photo = photo.Path_photo
                    };
                    Base.KE.Photos.Remove(photo);
                    Base.KE.Photos.Add(newPhoto);
                    Base.KE.SaveChanges();

                    Base.mainFrame.Navigate(new Account());
                }
            }
        }

        private void BtnBackPhoto_Click(object sender, RoutedEventArgs e)
        {
            IdCurrentPhoto--;
            Photos photo = ListPhoto[IdCurrentPhoto];

            if (photo != null)
            {
                imgPhoto.Source = Images.GetBitmapImage(photo);
            }

            if (IdCurrentPhoto == 0)
            {
                BtnBackPhoto.Visibility = Visibility.Hidden;
            }

            BtnNextPhoto.Visibility = Visibility.Visible;
        }

        private void BtnNextPhoto_Click(object sender, RoutedEventArgs e)
        {
            IdCurrentPhoto++;
            Photos photo = ListPhoto[IdCurrentPhoto];

            if (photo != null)
            {
                imgPhoto.Source = Images.GetBitmapImage(photo);
            }

            if (IdCurrentPhoto == ListPhoto.Count - 1)
            {
                BtnNextPhoto.Visibility = Visibility.Hidden;
            }

            BtnBackPhoto.Visibility = Visibility.Visible;
        }

        private void BtnChangePersonal_Click(object sender, RoutedEventArgs e)
        {
            AccountChangePersonal acp = new AccountChangePersonal(User);
            acp.ShowDialog();
            Base.mainFrame.Navigate(new Account());
        }

        private void BtnChangeAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountChange ac = new AccountChange(User);
            ac.ShowDialog();
        }

        private void BtnAddSomePhotos_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if ((bool)ofd.ShowDialog())
            {
                bool check = false;

                foreach (string path in ofd.FileNames)
                {
                    if (!Images.AddPhoto(path, User.Id_employee))
                    {
                        check = true;
                    }
                }

                Base.mainFrame.Navigate(new Account());

                if (check)
                {
                    MessageBox.Show("Часть фото не удалось загрузить! Обратитесь к администратору", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Фото успешно добавлены", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnViewEmp_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new ViewEmployees());
        }

        private void BtnViewChild_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new ViewChildren());
        }
    }
}