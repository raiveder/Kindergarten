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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Base.mainFrame = frm;
            Base.KE = new KindergartenEntities();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new Reg());
        }

        private void btnAvtor_Click(object sender, RoutedEventArgs e)
        {
            Base.mainFrame.Navigate(new Avtor());
        }
    }
}