using Entities;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.DAL.Repositories
{
    public class DbRepository : IUsersAndAwardsRepository
    {
        public DbRepository(string connectionString)
        {
            Users = new UserDb(connectionString);
            Awards = new AwardDb(connectionString);
        }
        public IRepository<User> Users { get; } 
        public IRepository<Award> Awards { get; }
        public void Save()
        {
            
        }
    }
}