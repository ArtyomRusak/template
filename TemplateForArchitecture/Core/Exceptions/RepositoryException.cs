using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class RepositoryException : Exception
    {
        protected RepositoryException()
        {

        }

        public RepositoryException(string message)
            : base(message)
        {

        }

        public RepositoryException(Exception exception)
            : base("See inner exception.", exception)
        {

        }
    }
}
