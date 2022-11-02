using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Детский_сад
{
    public partial class Parents
    {
        public string FullName
        {
            get
            {
                return Surname + " " + Names + " " + Patronymic;
            }
        }
    }
}