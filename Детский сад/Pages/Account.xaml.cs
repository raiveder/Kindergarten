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

            imgPhoto.Source = GetBitmapImage(user);
            imgPhoto.Stretch = Stretch.Uniform;
        }


        /// <summary>
        /// Находит главное (последнее) фото пользователя в базе данных.
        /// </summary>
        /// <param name="user">Пользователь (объект класса Employees).</param>
        /// <returns>Главное фото пользователя.</returns>
        private BitmapImage GetBitmapImage(Employees user)
        {
            List<Photos> list = Base.KE.Photos.Where(x => x.Id_employee == user.Id_employee).ToList();
            Photos photo = list.LastOrDefault(x => x.Id_employee == user.Id_employee);

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

            return null;
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Photos photo = new Photos();
                photo.Id_employee = User.Id_employee;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                string path = ofd.FileName;
                System.Drawing.Image sdi = System.Drawing.Image.FromFile(path);
                ImageConverter ic = new ImageConverter();
                byte[] array = (byte[])ic.ConvertTo(sdi, typeof(byte[]));
                photo.Byte_photo = array;
                Base.KE.Photos.Add(photo);
                Base.KE.SaveChanges();
                MessageBox.Show("Фото успешно добавлено", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
                Base.mainFrame.Navigate(new Account(User));

            }
            catch
            {
                MessageBox.Show("Возникла ошибка! Обратитесь к администратору", "Личный кабинет", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelPhoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}