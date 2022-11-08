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
using System.Text.RegularExpressions;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();

            cbGender.ItemsSource = Base.KE.Genders.ToList();
            cbGender.SelectedValuePath = "Id_gender";
            cbGender.DisplayMemberPath = "Gender";

            cbPositions.ItemsSource = Base.KE.Positions.ToList();
            cbPositions.SelectedValuePath = "Id_position";
            cbPositions.DisplayMemberPath = "Position";
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                Employees emp = new Employees()
                {
                    Surname = tboxSurname.Text,
                    Names = tboxName.Text,
                    Patronymic = tboxPatronymic.Text,
                    Birthdate = dpBirthdate.DisplayDate,
                    Id_gender = cbGender.SelectedIndex + 1,
                    Id_position = cbPositions.SelectedIndex + 1,
                    Street = tboxStreet.Text,
                    Building = tboxBuilding.Text,
                    Phone = tboxPhone.Text,
                    Login_user = tboxLogin.Text,
                    Password_user = tboxPassword.Text.GetHashCode(),
                    Id_role = 2
                };

                try
                {

                    Base.KE.Employees.Add(emp);
                    Base.KE.SaveChanges();
                    MessageBox.Show("Пользователь успешно зарегистрирован", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка! Данные не были занесены в базу", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool checkData()
        {
            Employees emp = Base.KE.Employees.FirstOrDefault(x => x.Login_user == tboxLogin.Text);

            if (!Regex.IsMatch(tboxSurname.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Фамилия должна начинаться с заглавной буквы и содержать только русские буквы", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxName.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Имя должно начинаться с заглавной буквы и содержать только русские буквы", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxPatronymic.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Отчество должно начинаться с заглавной буквы и содержать только русские буквы", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (dpBirthdate.DisplayDate == DateTime.Today)
            {
                MessageBox.Show("Выберите дату рождения", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (cbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пол", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (cbPositions.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите должность", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxStreet.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Улица должна начинаться с заглавной буквы и содержать только русские буквы", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (tboxBuilding.Text.Length == 0)
            {
                MessageBox.Show("Введите строение", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxPhone.Text, @"^\+7 9\d{2} \d{3}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Номер телефона должен соответствовать следующей маске: \"+7 9XX XXX-XX-XX\", где X - любая цифра", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (tboxLogin.Text.Length == 0)
            {
                MessageBox.Show("Введите логин", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (emp != null)
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (!checkPassword(tboxPassword.Text))
            {
                return false;
            }

            return true;
        }

        private bool checkPassword(string s)
        {
            if (tboxPassword.Text.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, "[A-Z]"))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 заглавный латинский символ", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, "[a-z]+.*[a-z]+.*[a-z]+"))
            {
                MessageBox.Show("Пароль должен содержать минимум 3 строчных латинских символов", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, @"\d+.*\d"))
            {
                MessageBox.Show("Пароль должен содержать минимум 2 цифры", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(s, @"\W"))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 специальный символ", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
    }
}