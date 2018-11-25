using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.DAL.Interfaces;
using Entities;


namespace UsersAndAwards.DAL.Repositories
{
    internal class AwardListRepository : ListRepository<Award>
    {
        public override Award Get(int id)
        {
            return Items.Find(a => a.Id == id);
        }

        protected override void SetId(Award v)
        {
            v.Id = CurId;
        }

        public override void Delete(int id)
        {
            var a = Items.Find(award => award.Id == id);
            if (a != null)
            {
                Items.Remove(a);
            }
        }
    }
}
