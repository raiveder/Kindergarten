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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
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
                Building = tboxBuilding.Text
            };
        }
    }
}
