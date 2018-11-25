using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.DAL.Repositories
{
    public class MemoryRepository : IUsersAndAwardsRepository
    {
        private UserListRepository _users;
        private AwardListRepository _awards;

        public IRepository<User> Users => _users ?? (_users = new UserListRepository());

        public IRepository<Award> Awards => _awards ?? (_awards = new AwardListRepository());
        public void Save()
        {
            
        }
    }
}
