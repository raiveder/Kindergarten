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

                DateTime dt = DateTime.Now;
                if (Birthdate.Month >= dt.Month)
                {
                    if (Birthdate.Day >= dt.Day)
                    {
                        years = Birthdate.Year - dt.Year;
                        months = Birthdate.Month - dt.Month;
                    }
                    years = Birthdate.Year - dt.Year - 1;
                    months = 12 - Birthdate.Month - 1 + dt.Month; //years наоборот
                }
                else
                {
                    years = Birthdate.Year - dt.Year - 1;
                    if (Birthdate.Day >= dt.Day)
                    {
                        months = 12 - Birthdate.Month - 1 + dt.Month;
                    }
                    months = 12 - Birthdate.Month - 2 + dt.Month;
                }


                return "";
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
    }
}