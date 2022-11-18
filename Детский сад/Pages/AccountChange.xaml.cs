using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для AccountChange.xaml
    /// </summary>
    public partial class AccountChange : Window
    {
        Employees User;

        public AccountChange(Employees user)
        {
            InitializeComponent();
            User = user;

            tboxLogin.Text = user.Login_user;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                User.Login_user = tboxLogin.Text;
                User.Password_user = tboxPassword.Text.GetHashCode();

                try
                {
                    Base.KE.SaveChanges();
                    Close();
                    MessageBox.Show("Данные успешно сохранены", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка! Данные не были обновлены", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool checkData()
        {
            Employees employees = Base.KE.Employees.FirstOrDefault(x => x.Login_user == tboxLogin.Text);

            if (tboxLogin.Text.Length == 0)
            {
                MessageBox.Show("Введите логин", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (employees != null && User.Id_employee != employees.Id_employee)
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!CheckPassword(tboxPassword.Text))
            {
                return false;
            }
            else if (tboxPassword.Text != tboxRepeatPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Валидация пароля
        /// </summary>
        /// <param name="s">Пароль</param>
        /// <returns>Валидация прошла успешно - true, иначе - false</returns>
        private bool CheckPassword(string s)
        {
            if (tboxPassword.Text.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, "[A-Z]"))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 заглавный латинский символ", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, "[a-z]+.*[a-z]+.*[a-z]+"))
            {
                MessageBox.Show("Пароль должен содержать минимум 3 строчных латинских символов", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, @"\d+.*\d"))
            {
                MessageBox.Show("Пароль должен содержать минимум 2 цифры", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, @"\W"))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 специальный символ", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
    }
}