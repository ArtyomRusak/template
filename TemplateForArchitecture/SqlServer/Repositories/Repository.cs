using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.InterfaceRepository;

namespace SqlServer.Repositories
{
    public class Repository : IRepository
    {
        protected DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }
    }
}
