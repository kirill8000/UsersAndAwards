using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Award> Awards { get; set; } = new List<Award>();
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var y = today.Year - BirthDate.Year;
                if (BirthDate > today.AddYears(-y)) y--;
                return y;
            }
        }

        public string AwardsString => string.Join(", ", Awards);
    }
}
