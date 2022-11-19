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
    /// Логика взаимодействия для Avtor.xaml
    /// </summary>
    public partial class Avtor : Page
    {
        public Avtor()
        {
            InitializeComponent();
        }

        private void btnAvtor_Click(object sender, RoutedEventArgs e)
        {
            Employees emp = Base.KE.Employees.FirstOrDefault(x => x.Login_user == tboxLogin.Text);
            if (emp == null)
            {
                MessageBox.Show("Пользователя с таким логином не существует", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (emp.Password_user != pbPassword.Password.GetHashCode())
            {
                MessageBox.Show("Неверный пароль", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (emp.Id_role == 1)
            {
                Base.User = emp;
                Base.mainFrame.Navigate(new Account(emp));
            }
            else if (emp.Id_role == 2)
            {
                Base.mainFrame.Navigate(new Account(emp));
            }
        }
    }
}