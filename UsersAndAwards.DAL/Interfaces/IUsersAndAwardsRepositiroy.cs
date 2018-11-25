using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace UsersAndAwards.DAL.Interfaces
{
    public interface IUsersAndAwardsRepository
    {
        IRepository<User> Users { get; }
        IRepository<Award> Awards { get; }
        void Save();
    }
}
