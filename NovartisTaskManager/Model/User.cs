using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovartisTaskManager.BusinessClass
{
    public class User
    {
        public string ID
        {
            set;
            get;
        }
        public int Type
        {
            set;
            get;
        }
        public string Name
        {
            get;
            set;
        }
        public string Team
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }

    }
}
