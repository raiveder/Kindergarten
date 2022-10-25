using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Детский_сад
{
    internal class Base
    {
        public static KindergartenEntities KE;
        public static Frame mainFrame;
        public static Button btnAvtor;
        public static Button btnReg;
        public static Button btnMain;

        public static void tranzitionMain()
        {
            btnMain.Visibility = Visibility.Collapsed;
            btnAvtor.Visibility = Visibility.Visible;
            btnReg.Visibility = Visibility.Visible;
        }
        public static void leaveMain()
        {
            btnAvtor.Visibility = Visibility.Collapsed;
            btnReg.Visibility = Visibility.Collapsed;
            btnMain.Visibility = Visibility.Visible;
        }
    }
}
