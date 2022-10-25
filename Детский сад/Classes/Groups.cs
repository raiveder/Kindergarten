using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Детский_сад
{
    public partial class Groups
    {
        public string Name_group
        {
            get
            {
                return Code + "-я " + Types_group.Type_group.ToLower();
            }
        }
    }
}
