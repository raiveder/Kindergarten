using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

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
                    return Brushes.PaleVioletRed;
                }
                else
                {
                    return Brushes.PapayaWhip;
                }
            }
        }

        public string Age
        {
            get
            {
                int years;
                int months;
                getAge(out years, out months);

                if (years == 0)
                {
                    return months + " мес.";
                }

                if (months == 0)
                {
                    return years + " г.";
                }

                return years + " г. " + months + " мес.";
            }
        }

        public string FullName
        {
            get
            {
                return Surname + " " + Names + " " + Patronymic;
            }
        }

        public string Adress
        {
            get
            {
                return "ул. " + Street + ", " + Building;
            }
        }

        private void getAge(out int years, out int months)
        {
            DateTime dt = DateTime.Now;
            if (Birthdate.Month > dt.Month)
            {
                years = dt.Year - Birthdate.Year - 1;
                if (Birthdate.Day > dt.Day)
                {
                    months = 12 - Birthdate.Month + dt.Month - 1;
                }
                else
                {
                    months = 12 - Birthdate.Month + dt.Month;
                }
            }
            else if (Birthdate.Month == dt.Month)
            {
                if (Birthdate.Day > dt.Day)
                {
                    years = dt.Year - Birthdate.Year - 1;
                    months = 11;
                }
                else
                {
                    years = dt.Year - Birthdate.Year;
                    months = Birthdate.Month - dt.Month;
                }
            }
            else
            {
                years = dt.Year - Birthdate.Year;
                if (Birthdate.Day > dt.Day)
                {
                    months = dt.Month - Birthdate.Month - 1;
                }
                else
                {
                    months = dt.Month - Birthdate.Month;
                }
            }
        }
    }
}