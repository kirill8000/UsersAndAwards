using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using UsersAndAwards.BLL.Interfaces;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.BLL.Services
{
    public class AwardService : IAwardService
    {
        protected IUsersAndAwardsRepository Repository;

        public AwardService(IUsersAndAwardsRepository repository)
        {
            Repository = repository;
        }
        public void ToAward(int userId, int awardId)
        {
            var user = Repository.Users.Get(userId);
            var award = Repository.Awards.Get(awardId);
            user.Awards.Add(award);
            Repository.Users.Update(user);
        }

        public void AddUser(User user)
        {
            Repository.Users.Create(user);
        }

        public void AddAward(Award award)
        {
            Repository.Awards.Create(award);
        }

        public IEnumerable<Award> GetAwards(int userId)
        {
            return Repository.Users.Get(userId).Awards;
        }

       

        public IEnumerable<Award> Awards => Repository.Awards.GetAll();
        public IEnumerable<User> Users => Repository.Users.GetAll();

        public IEnumerable<Award> GetPossibleAwards(int userId)
        {
            foreach (var award in Awards)
            {
                if (!Repository.Users.Get(userId).Awards.Exists(award1 => award1.Id == award.Id))
                    yield return award;
            }
        }

        public void RemoveAward(int awardId)
        {
            var award = Repository.Awards.Get(awardId);
            //var users = Repository.Users.Find(user => user.Awards.Contains(award));
            foreach (var user in Repository.Users.GetAll())
            {

                if (user.Awards.Exists(award1 => award1.Id == awardId))
                {
                    user.Awards.Remove(award);
                    Repository.Users.Update(user);
                }
            }

            Repository.Awards.Delete(awardId);
        }

        public void RemoveUser(int userId)
        {
            Repository.Users.Delete(userId);
        }

        public User GetUser(int userId)
        {
            return Repository.Users.Get(userId);
        }

        public Award GetAward(int awardId)
        {
            return Repository.Awards.Get(awardId);
        }

        public void UpdateUser(User user)
        {
            Repository.Users.Update(user);
        }
    }
}
