using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSoftwareDevCA
{
    class CustomersList : ICloneable
    {
        public List<Customer> customers { get; }

        public CustomersList(List<Customer> customers)
        {
            this.customers = customers;
        }

        public Object Clone()
        {
            return this.MemberwiseClone() as CustomersList;
        }

       
    }
}
