using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Xml.Serialization;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        Employees User;
        List<Photos> ListPhoto;
        int IdCurrentPhoto;

        public Account(Employees user)
        {
            InitializeComponent();

            User = user;

            tbFullName.Text += user.Surname + " " + user.Names + " " + user.Patronymic;
            tbBirthdate.Text += user.Birthdate.ToString("D");
            tbPosition.Text += user.Positions.Position;
            tbAdress.Text += user.Building.ToLower() + ", " + user.Building;
            tbLogin.Text += user.Login_user;
            tbRole.Text += "пользователь";

            List<Distributions> list = Base.KE.Distributions.Where(x => x.Id_employee == user.Id_employee).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                tbGroups.Text += list[i].Groups.Name_group + ", ";
                if (i % 2 == 1 && i != list.Count - 1)
                {
                    tbGroups.Text += "\n";
                }
            }

            tbGroups.Text = tbGroups.Text.Remove(tbGroups.Text.Length - 2, 2);

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

            imgPhoto.Source = GetBitmapImage(photo);
            imgPhoto.Stretch = Stretch.Uniform;
        }

        private BitmapImage GetBitmapImage(Photos photo)
        {
            if (photo != null)
            {
                byte[] array = photo.Byte_photo;
                BitmapImage image = new BitmapImage();

                using (MemoryStream ms = new MemoryStream(array))
                {
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }

                return image;
            }

            return new BitmapImage(new Uri("\\Resources\\Заглушка.png", UriKind.RelativeOrAbsolute));
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Photos photo = new Photos();
                photo.Id_employee = User.Id_employee;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();

                if (ofd.FileName != "")
                {
                    string path = ofd.FileName;

                    System.Drawing.Image sdi = System.Drawing.Image.FromFile(path);
                    ImageConverter ic = new ImageConverter();

                    byte[] array = (byte[])ic.ConvertTo(sdi, typeof(byte[]));
                    photo.Byte_photo = array;

                    Base.KE.Photos.Add(photo);
                    Base.KE.SaveChanges();

                    Base.mainFrame.Navigate(new Account(User));
                    MessageBox.Show("Фото успешно добавлено", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка! Обратитесь к администратору", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelPhoto_Click(object sender, RoutedEventArgs e)
        {
            
            if(IdCurrentPhoto == -1)
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
                btnBackPhoto.Visibility = Visibility.Hidden;
            }
            else
            {
                if (IdCurrentPhoto == 0)
                {
                    btnBackPhoto.Visibility = Visibility.Hidden;
                }

                photo = ListPhoto[IdCurrentPhoto];
            }
            imgPhoto.Source = GetBitmapImage(photo);
        }

        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (btnChangePhoto.Content.ToString() == "Изменить")
            {
                btnChangePhoto.Content = "Сохранить";
                if (IdCurrentPhoto != -1)
                {
                    btnBackPhoto.Visibility = Visibility.Visible;
                }

            }
            else
            {
                btnChangePhoto.Content = "Изменить";
                btnBackPhoto.Visibility = Visibility.Hidden;
                btnNextPhoto.Visibility = Visibility.Hidden;

                if (IdCurrentPhoto != -1)
                {
                    Photos photo = ListPhoto[IdCurrentPhoto];
                    Photos newPhoto = new Photos()
                    {
                        Id_employee = photo.Id_employee,
                        Id_children = photo.Id_children,
                        Byte_photo = photo.Byte_photo,
                        Path_photo = photo.Path_photo
                    };
                    Base.KE.Photos.Remove(photo);
                    Base.KE.Photos.Add(newPhoto);
                    Base.KE.SaveChanges();

                    Base.mainFrame.Navigate(new Account(User));
                }

            }
        }

        private void btnBackPhoto_Click(object sender, RoutedEventArgs e)
        {
            IdCurrentPhoto--;
            Photos photo = ListPhoto[IdCurrentPhoto];

            if (photo != null)
            {
                imgPhoto.Source = GetBitmapImage(photo);
            }

            if (IdCurrentPhoto == 0)
            {
                btnBackPhoto.Visibility = Visibility.Hidden;
            }

            btnNextPhoto.Visibility = Visibility.Visible;
        }

        private void btnNextPhoto_Click(object sender, RoutedEventArgs e)
        {
            IdCurrentPhoto++;
            Photos photo = ListPhoto[IdCurrentPhoto];

            if (photo != null)
            {
                imgPhoto.Source = GetBitmapImage(photo);
            }

            if (IdCurrentPhoto == ListPhoto.Count - 1)
            {
                btnNextPhoto.Visibility = Visibility.Hidden;
            }

            btnBackPhoto.Visibility = Visibility.Visible;
        }

        private void btnChangePersonal_Click(object sender, RoutedEventArgs e)
        {
            AccountChangePersonal acp = new AccountChangePersonal(User);
            acp.ShowDialog();
            Base.mainFrame.Navigate(new Account(User));
        }
    }
}