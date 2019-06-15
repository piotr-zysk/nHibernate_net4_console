using nHibernate.entities;
using System.Collections.Generic;

namespace nHibernate.core.webapi.Repositories
{
    public interface IUserRepository : IRepository
    {
        int GetNumberofUsers();

        IEnumerable<User> GetAll();
    }
}