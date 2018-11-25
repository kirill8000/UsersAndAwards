using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace UsersAndAwards.BLL.Interfaces
{
    public interface IAwardService
    {
        void ToAward(int userId, int awardId);
        void AddUser(User user);
        void AddAward(Award award);
        IEnumerable<Award> GetAwards(int userId);
        IEnumerable<Award> Awards { get; }
        IEnumerable<User> Users { get; }
        IEnumerable<Award> GetPossibleAwards(int userId);
        void RemoveAward(int awardId);
        void RemoveUser(int awardId);
        User GetUser(int userId);
        Award GetAward(int awardId);
        void UpdateUser(User user);
    }
}
