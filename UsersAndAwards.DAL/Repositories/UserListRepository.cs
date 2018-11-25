using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.DAL.Repositories
{
    internal class UserListRepository : ListRepository<User>
    {

        public override User Get(int id)
        {
            return Items.Find(user => user.Id == id);
        }

        protected override void SetId(User v)
        {
            v.Id = CurId;
        }

        public override void Delete(int id)
        {
            var u = Items.Find(user => user.Id == id);
            if (u != null)
            {
                Items.Remove(u);
            }
        }
    }
}
