using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSoftwareDevCA
{
    [Serializable]
    class User
    {
        public string ID { get;  }
        public string FirstName { get;  }
        public string LastName { get; }
        public string EmailAddress { get; }
    }
}
