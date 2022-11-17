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
    /// Логика взаимодействия для AccountChangePersonal.xaml
    /// </summary>
    public partial class AccountChangePersonal : Window
    {
        Employees User;
        public AccountChangePersonal(Employees user)
        {
            InitializeComponent();

            User = user;

            tboxSurname.Text = user.Surname;
            tboxName.Text = user.Names;
            tboxPatronymic.Text = user.Patronymic;
            dpBirthdate.SelectedDate = user.Birthdate;
            tboxStreet.Text = user.Street;
            tboxBuilding.Text = user.Building;
            tboxPhone.Text = user.Phone;

            dpBirthdate.DisplayDateStart = new DateTime(DateTime.Now.Year - 70, 1, 1);
            dpBirthdate.DisplayDateEnd = new DateTime(DateTime.Now.Year - 16, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                User.Surname = tboxSurname.Text;
                User.Names = tboxName.Text;
                User.Patronymic = tboxPatronymic.Text;
                User.Birthdate = dpBirthdate.SelectedDate.Value;
                User.Street = tboxStreet.Text;
                User.Building = tboxBuilding.Text;
                User.Phone = tboxPhone.Text;

                Base.KE.SaveChanges();
                Close();
            }
        }

        private bool checkData()
        {
            if (!Regex.IsMatch(tboxSurname.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Фамилия должна начинаться с заглавной буквы и содержать только русские буквы", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxName.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Имя должно начинаться с заглавной буквы и содержать только русские буквы", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxPatronymic.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Отчество должно начинаться с заглавной буквы и содержать только русские буквы", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (dpBirthdate.SelectedDate.Value == DateTime.Today)
            {
                MessageBox.Show("Выберите дату рождения", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxStreet.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Улица должна начинаться с заглавной буквы и содержать только русские буквы", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (tboxBuilding.Text.Length == 0)
            {
                MessageBox.Show("Введите строение", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxPhone.Text, @"^\+7 9\d{2} \d{3}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Номер телефона должен соответствовать следующей маске: \"+7 9XX XXX-XX-XX\", где X - любая цифра", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
    }
}