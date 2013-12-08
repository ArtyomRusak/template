using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class ServiceException : Exception
    {
        protected ServiceException()
        {

        }

        public ServiceException(string message)
            : base(message)
        {

        }

        public ServiceException(Exception exception)
            : base("See inner exception", exception)
        {

        }
    }
}
