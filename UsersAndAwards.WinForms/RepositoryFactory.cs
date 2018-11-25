using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.DAL.Interfaces;
using UsersAndAwards.DAL.Repositories;

namespace UsersAndAwards.WinForms
{
    static class RepositoryFactory
    {
        public static IUsersAndAwardsRepository GetRepository()
        {
            var dataSource = ConfigurationManager.AppSettings.Get("DataSource");
            switch (dataSource)
            {
                case "DB":
                    var cs = ConfigurationManager.ConnectionStrings["DbConnect"];
                    return new DbRepository(cs.ConnectionString);
                case "Memory":
                    return new MemoryRepository();
                default:
                    return new MemoryRepository();
            }
        }
    }
}
