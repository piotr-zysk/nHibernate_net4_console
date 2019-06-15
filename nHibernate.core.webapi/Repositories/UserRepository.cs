﻿using NHibernate;
using nHibernate.entities;
using System.Collections.Generic;
using System.Linq;

namespace nHibernate.core.webapi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession session;
        public UserRepository(NHibernate.ISession session)
        {
            this.session = session;
        }

        public IEnumerable<User> GetAll()
            => session.Query<User>().ToList();

        public int GetNumberofUsers()
            => session.Query<User>().Count();
    }
}
