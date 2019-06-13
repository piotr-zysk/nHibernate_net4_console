using nHibernate_entities;
using System.Collections.Generic;

namespace nHibernate.core.webapi.Repositories
{
    public interface IUserRepository
    {
        int GetNumberofUsers();

        IEnumerable<User> GetAll();
    }
}