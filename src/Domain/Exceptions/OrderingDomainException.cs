using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailInMotion.Domain.Exceptions
{
    public class OrderingDomainException : Exception
    {
        public OrderingDomainException(string message)
            : base(message)
        {
        }
    }
}