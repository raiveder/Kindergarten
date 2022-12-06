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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Детский_сад
{
    /// <summary>
    /// Логика взаимодействия для Advertisement.xaml
    /// </summary>
    public partial class Advertisement : Page
    {
        public Advertisement()
        {
            InitializeComponent();

            DoubleAnimation tbFontAnimation = new DoubleAnimation();
            tbFontAnimation.From = 20;
            tbFontAnimation.To = 30;
            tbFontAnimation.Duration = TimeSpan.FromSeconds(1);
            tbFontAnimation.RepeatBehavior = RepeatBehavior.Forever;
            tbFontAnimation.AutoReverse = true;
            tbName.BeginAnimation(FontSizeProperty, tbFontAnimation);

            DoubleAnimation imageWidthAnimation = new DoubleAnimation();
            imageWidthAnimation.From = 150;
            imageWidthAnimation.To = 200;
            imageWidthAnimation.Duration = TimeSpan.FromSeconds(1);
            imageWidthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            imageWidthAnimation.AutoReverse = true;
            img.BeginAnimation(WidthProperty, imageWidthAnimation);

            DoubleAnimation btnWidthAnimation = new DoubleAnimation();
            btnWidthAnimation.From = 170;
            btnWidthAnimation.To = 200;
            btnWidthAnimation.Duration = TimeSpan.FromSeconds(1);
            btnWidthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btnWidthAnimation.AutoReverse = true;
            btn.BeginAnimation(WidthProperty, btnWidthAnimation);

            DoubleAnimation btnHeightAnimation = new DoubleAnimation();
            btnHeightAnimation.From = 50;
            btnHeightAnimation.To = 70;
            btnHeightAnimation.Duration = TimeSpan.FromSeconds(1);
            btnHeightAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btnHeightAnimation.AutoReverse = true;
            btn.BeginAnimation(HeightProperty, btnHeightAnimation);

            ThicknessAnimation btnThicknessAnimation = new ThicknessAnimation();
            btnThicknessAnimation.From = new Thickness(0, 0, 190, 30);
            btnThicknessAnimation.To = new Thickness(190, 30, 0, 0);
            btnThicknessAnimation.Duration = TimeSpan.FromSeconds(2);
            btnHeightAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btnHeightAnimation.AutoReverse = true;
            btn.BeginAnimation(MarginProperty, btnThicknessAnimation);

            ColorAnimation btnBackgroundAnimation = new ColorAnimation();
            Color Cstart = Color.FromRgb(255, 0, 0);
            btn.Background = new SolidColorBrush(Cstart);
            btnBackgroundAnimation.From = Color.FromRgb(248, 168, 27);
            btnBackgroundAnimation.To = Color.FromRgb(0, 175, 176);
            btnBackgroundAnimation.Duration = TimeSpan.FromSeconds(2);
            btnBackgroundAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btnBackgroundAnimation.AutoReverse = true;
            btn.Background.BeginAnimation(SolidColorBrush.ColorProperty, btnBackgroundAnimation);
        }
    }
}