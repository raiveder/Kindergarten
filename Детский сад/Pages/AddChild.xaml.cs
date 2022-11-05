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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для AddChild.xaml
    /// </summary>
    public partial class AddChild : Page
    {
        public AddChild()
        {
            InitializeComponent();

            cbGender.ItemsSource = Base.KE.Genders.ToList();
            cbGender.SelectedValuePath = "Id_gender";
            cbGender.DisplayMemberPath = "Gender";

            cbGroup.ItemsSource = Base.KE.Groups.ToList();
            cbGroup.SelectedValuePath = "Id_group";
            cbGroup.DisplayMemberPath = "Name_group";
        }

        string path = "Resources\\Заглушка.png";

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkClild() && checkMother() && checkFather())
            {
                Children child = new Children()
                {
                    Surname = tboxSurname.Text,
                    Names = tboxName.Text,
                    Patronymic = tboxPatronymic.Text,
                    Birthdate = dpBirthdate.DisplayDate,
                    Id_gender = cbGender.SelectedIndex + 1,
                    Id_group = cbGroup.SelectedIndex + 1,
                    Street = tboxStreet.Text,
                    Building = tboxBuilding.Text,
                    Photo = path
                };
                Base.KE.Children.Add(child);
                Base.KE.SaveChanges();

                if (chBMother.IsChecked == true)
                {
                    Parents mother = new Parents()
                    {
                        Surname = tboxMotherSurname.Text,
                        Names = tboxMotherName.Text,
                        Patronymic = tboxMotherPatronymic.Text,
                        Birthdate = dpMotherBirthdate.DisplayDate,
                        Id_gender = 2,
                        Street = tboxMotherStreet.Text,
                        Building = tboxMotherBuilding.Text,
                        Phone = tboxMotherPhone.Text
                    };
                    Base.KE.Parents.Add(mother);
                    Base.KE.SaveChanges();

                    Kinships kinships = new Kinships()
                    {
                        Id_parent = mother.Id_parent,
                        Id_child = child.Id_child
                    };

                    Base.KE.Kinships.Add(kinships);
                }

                if (chBFather.IsChecked == true)
                {
                    Parents father = new Parents()
                    {
                        Surname = tboxFatherSurname.Text,
                        Names = tboxFatherName.Text,
                        Patronymic = tboxFatherPatronymic.Text,
                        Birthdate = dpFatherBirthdate.DisplayDate,
                        Id_gender = 1,
                        Street = tboxFatherStreet.Text,
                        Building = tboxFatherBuilding.Text,
                        Phone = tboxFatherPhone.Text
                    };
                    Base.KE.Parents.Add(father);
                    Base.KE.SaveChanges();

                    Kinships kinships = new Kinships()
                    {
                        Id_parent = father.Id_parent,
                        Id_child = child.Id_child
                    };

                    Base.KE.Kinships.Add(kinships);
                }

                Base.KE.SaveChanges();
            }
        }

        private bool checkClild()
        {
            if (!Regex.IsMatch(tboxSurname.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Фамилия должна начинаться с заглавной буквы и содержать только русские буквы", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxName.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Имя должно начинаться с заглавной буквы и содержать только русские буквы", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxPatronymic.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Отчество должно начинаться с заглавной буквы и содержать только русские буквы", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (dpBirthdate.DisplayDate == DateTime.Today)
            {
                MessageBox.Show("Выберите дату рождения", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (cbGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите группу", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (cbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите группу", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxStreet.Text, @"^[А-Я][а-я]+$"))
            {
                MessageBox.Show("Улица должна начинаться с заглавной буквы и содержать только русские буквы", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (tboxBuilding.Text.Length == 0)
            {
                MessageBox.Show("Введите строение", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (chBMother.IsChecked == false && chBFather.IsChecked == false)
            {
                MessageBox.Show("Выберите хотя бы одного родителя", "Родители", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private bool checkMother()
        {
            if (chBMother.IsChecked == true)
            {
                if (!Regex.IsMatch(tboxMotherSurname.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Фамилия должна начинаться с заглавной буквы и содержать только русские буквы", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxMotherName.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Имя должно начинаться с заглавной буквы и содержать только русские буквы", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxMotherPatronymic.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Отчество должно начинаться с заглавной буквы и содержать только русские буквы", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxMotherStreet.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Улица должна начинаться с заглавной буквы и содержать только русские буквы", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (tboxMotherBuilding.Text.Length == 0)
                {
                    MessageBox.Show("Введите строение", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxMotherPhone.Text, @"^\+7 9\d{2} \d{3}-\d{2}-\d{2}$"))
                {
                    MessageBox.Show("Номер телефона должен соответствовать следующей маске: \"+7 9XX XXX-XX-XX\", где X - любая цифра", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (dpMotherBirthdate.DisplayDate == DateTime.Today)
                {
                    MessageBox.Show("Выберите дату рождения", "Мать", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }

        private bool checkFather()
        {
            if (chBFather.IsChecked == true)
            {
                if (!Regex.IsMatch(tboxFatherSurname.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Фамилия должна начинаться с заглавной буквы и содержать только русские буквы", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxFatherName.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Имя должно начинаться с заглавной буквы и содержать только русские буквы", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxFatherPatronymic.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Отчество должно начинаться с заглавной буквы и содержать только русские буквы", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxFatherStreet.Text, @"^[А-Я][а-я]+$"))
                {
                    MessageBox.Show("Улица должна начинаться с заглавной буквы и содержать только русские буквы", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (tboxFatherBuilding.Text.Length == 0)
                {
                    MessageBox.Show("Введите строение", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (!Regex.IsMatch(tboxFatherPhone.Text, @"^\+7 9\d{2} \d{3}-\d{2}-\d{2}$"))
                {
                    MessageBox.Show("Номер телефона должен соответствовать следующей маске: \"+7 9XX XXX-XX-XX\", где X - любая цифра", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                else if (dpFatherBirthdate.DisplayDate == DateTime.Today)
                {
                    MessageBox.Show("Выберите дату рождения", "Отец", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new ViewChildren());
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string[] array = ofd.FileName.Split('\\');
            if (array.Length != 1)
            {
                path = "\\" + array[array.Length - 2] + "\\" + array[array.Length - 1];
                imgPhoto.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            }
        }
    }
}