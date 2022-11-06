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
using System.Deployment.Internal;
using System.Windows.Markup;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для AddChild.xaml
    /// </summary>
    public partial class AddChild : Page
    {
        string Path = "\\Resources\\Заглушка.png";
        Children Child;
        Sertificates Sertificate;
        bool FlagCreate = true;

        public AddChild()
        {
            uploadPage();
        }

        public AddChild(Children child)
        {
            uploadPage();
            btnAdd.Content = "Сохранить";

            fillChild(child);
            fillParents(Child.Id_child);
            FlagCreate = false;
        }

        private void fillChild(Children child)
        {
            Child = child;
            tboxSurname.Text = Child.Surname;
            tboxName.Text = Child.Names;
            tboxPatronymic.Text = Child.Patronymic;
            dpBirthdate.SelectedDate = Child.Birthdate;
            cbGender.SelectedIndex = Child.Id_gender - 1;
            cbGroup.SelectedIndex = Child.Id_group - 1;
            tboxStreet.Text = Child.Street;
            tboxBuilding.Text = Child.Building;
            Path = Child.Photo;
            imgPhoto.Source = new BitmapImage(new Uri(Path, UriKind.Relative));

            Sertificate = Base.KE.Sertificates.FirstOrDefault(x => x.Id_sertificate == Child.Id_child);
            tboxSeries.Text = Sertificate.Series;
            tboxNumber.Text = Sertificate.Number.ToString();
            dpDateIssue.SelectedDate = Sertificate.Date_issue;
            tboxIddued.Text = Sertificate.Iddued;
        }

        private void fillParents(int idChild)
        {
            List<Kinships> kinships = Base.KE.Kinships.Where(x => x.Id_child == Child.Id_child).ToList();

            foreach (Kinships item in kinships)
            {
                if (item.Parents.Id_gender == 2)
                {
                    chBMother.IsChecked = true;
                    spMother1.Visibility = Visibility.Visible;
                    spMother2.Visibility = Visibility.Visible;
                    spMother3.Visibility = Visibility.Visible;

                    tboxMotherSurname.Text = item.Parents.Surname;
                    tboxMotherName.Text = item.Parents.Names;
                    tboxMotherPatronymic.Text = item.Parents.Patronymic;
                    tboxMotherPatronymic.Text = item.Parents.Patronymic;
                    dpMotherBirthdate.SelectedDate = item.Parents.Birthdate;
                    cbGender.SelectedIndex = item.Parents.Id_gender - 1;
                    tboxMotherStreet.Text = item.Parents.Street;
                    tboxMotherBuilding.Text = item.Parents.Building;
                    tboxMotherPhone.Text = item.Parents.Phone;
                }
                else
                {
                    chBFather.IsChecked = true;
                    spFather1.Visibility = Visibility.Visible;
                    spFather2.Visibility = Visibility.Visible;
                    spFather3.Visibility = Visibility.Visible;

                    tboxFatherSurname.Text = item.Parents.Surname;
                    tboxFatherName.Text = item.Parents.Names;
                    tboxFatherPatronymic.Text = item.Parents.Patronymic;
                    tboxFatherPatronymic.Text = item.Parents.Patronymic;
                    dpFatherBirthdate.SelectedDate = item.Parents.Birthdate;
                    cbGender.SelectedIndex = item.Parents.Id_gender - 1;
                    tboxFatherStreet.Text = item.Parents.Street;
                    tboxFatherBuilding.Text = item.Parents.Building;
                    tboxFatherPhone.Text = item.Parents.Phone;
                }
            }
        }

        private void uploadPage()
        {
            InitializeComponent();

            cbGender.ItemsSource = Base.KE.Genders.ToList();
            cbGender.SelectedValuePath = "Id_gender";
            cbGender.DisplayMemberPath = "Gender";

            cbGroup.ItemsSource = Base.KE.Groups.ToList();
            cbGroup.SelectedValuePath = "Id_group";
            cbGroup.DisplayMemberPath = "Name_group";

            spMother1.Visibility = Visibility.Hidden;
            spMother2.Visibility = Visibility.Hidden;
            spMother3.Visibility = Visibility.Hidden;

            spFather1.Visibility = Visibility.Hidden;
            spFather2.Visibility = Visibility.Hidden;
            spFather3.Visibility = Visibility.Hidden;

            dpBirthdate.DisplayDateStart = new DateTime(DateTime.Now.Year - 8, 1, 1);
            dpBirthdate.DisplayDateEnd = DateTime.Now;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkClild() && checkSertificate() && checkMother() && checkFather())
            {
                if (FlagCreate)
                {
                    Child = new Children();
                    Sertificate = new Sertificates();
                }

                Child.Surname = tboxSurname.Text;
                Child.Names = tboxName.Text;
                Child.Patronymic = tboxPatronymic.Text;
                Child.Birthdate = dpBirthdate.DisplayDate;
                Child.Id_gender = cbGender.SelectedIndex + 1;
                Child.Id_group = cbGroup.SelectedIndex + 1;
                Child.Street = tboxStreet.Text;
                Child.Building = tboxBuilding.Text;
                Child.Photo = Path;

                Sertificate.Id_sertificate = Child.Id_child;
                Sertificate.Series = tboxSeries.Text;
                Sertificate.Number = Convert.ToInt32(tboxNumber.Text);
                Sertificate.Date_issue = dpDateIssue.DisplayDate;
                Sertificate.Iddued = tboxIddued.Text;

                if (FlagCreate)
                {
                    Base.KE.Children.Add(Child);
                }
                Base.KE.SaveChanges();

                if (FlagCreate)
                {
                    Base.KE.Sertificates.Add(Sertificate);
                }
                Base.KE.SaveChanges();


                if (!FlagCreate)
                {
                    RemoveFromKingships(Child.Id_child);
                }

                int idParent = 0;

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

                    if (CheckRepeat(mother, ref idParent))
                    {
                        Base.KE.Parents.Add(mother);
                        Base.KE.SaveChanges();
                        idParent = mother.Id_parent;
                    }

                    Kinships kinships = new Kinships()
                    {
                        Id_parent = idParent,
                        Id_child = Child.Id_child
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

                    if (CheckRepeat(father, ref idParent))
                    {
                        Base.KE.Parents.Add(father);
                        Base.KE.SaveChanges();
                        idParent = father.Id_parent;
                    }

                    Kinships kinships = new Kinships()
                    {
                        Id_parent = idParent,
                        Id_child = Child.Id_child
                    };

                    Base.KE.Kinships.Add(kinships);
                }

                Base.KE.SaveChanges();
                
                if (FlagCreate)
                {
                    MessageBox.Show("Ребёнок успешно добавлен", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Данные изменены", "Ребёнок", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool CheckRepeat(Parents parent, ref int id)
        {
            foreach (Parents item in Base.KE.Parents)
            {
                if (item.FullName == parent.FullName && item.Birthdate == parent.Birthdate && item.Id_gender == parent.Id_gender &&
                    item.Street == parent.Street && item.Building == parent.Building && item.Phone == parent.Phone)
                {
                    id = item.Id_parent;
                    return false;
                }
            }
            return true;
        }

        private void RemoveFromKingships(int idChild)
        {
            List<Kinships> kinships = Base.KE.Kinships.Where(x => x.Id_child == idChild).ToList();

            foreach (Kinships item in kinships)
            {
                List<Kinships> kinshipsRemove = Base.KE.Kinships.Where(x => x.Id_parent == item.Id_parent && x.Id_child != idChild).ToList();

                if (kinshipsRemove.Count == 0)
                {
                    Base.KE.Parents.Remove(Base.KE.Parents.FirstOrDefault(x => x.Id_parent == item.Id_parent));
                }

                Base.KE.Kinships.Remove(item);
            }

            Base.KE.SaveChanges();
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

            return true;
        }

        private bool checkSertificate()
        {
            if (!Regex.IsMatch(tboxSeries.Text, @"^((I{1,3})|(I?V)|(VI{1,3}))-[А-Я]{2}$"))
            {
                MessageBox.Show("Серия должна иметь следующий формат: римские цифры (I или V), тире, 2 заглавные русские буквы (пример: IV-АГ, III-ШБ)", "Свидетельство о рождении", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (!Regex.IsMatch(tboxNumber.Text, @"^[0-9]{6}$"))
            {
                MessageBox.Show("Номер должен состоять из 6 цифр", "Свидетельство о рождении", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (dpDateIssue.DisplayDate == DateTime.Today)
            {
                MessageBox.Show("Выберите дату выдачи свидетельства", "Свидетельство о рождении", MessageBoxButton.OK, MessageBoxImage.Information);
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
                Path = "\\" + array[array.Length - 2] + "\\" + array[array.Length - 1];
                imgPhoto.Source = new BitmapImage(new Uri(Path, UriKind.Relative));
            }
        }

        private void chBMother_Click(object sender, RoutedEventArgs e)
        {
            if (chBMother.IsChecked == true)
            {
                spMother1.Visibility = Visibility.Visible;
                spMother2.Visibility = Visibility.Visible;
                spMother3.Visibility = Visibility.Visible;
            }
            else
            {
                spMother1.Visibility = Visibility.Hidden;
                spMother2.Visibility = Visibility.Hidden;
                spMother3.Visibility = Visibility.Hidden;
            }
        }

        private void chBFather_Click(object sender, RoutedEventArgs e)
        {
            if (chBFather.IsChecked == true)
            {
                spFather1.Visibility = Visibility.Visible;
                spFather2.Visibility = Visibility.Visible;
                spFather3.Visibility = Visibility.Visible;
            }
            else
            {
                spFather1.Visibility = Visibility.Hidden;
                spFather2.Visibility = Visibility.Hidden;
                spFather3.Visibility = Visibility.Hidden;
            }
        }
    }
}