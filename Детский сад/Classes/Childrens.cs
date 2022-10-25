using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Детский_сад
{
    public partial class Children
    {
        public SolidColorBrush ChildrenColor
        {
            get
            {
                if (Birthdate.Month == DateTime.Today.Month && Birthdate.Day == DateTime.Today.Day)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.White;
                }
            }
        }
    }
}
