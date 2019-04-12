using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Fried_Chicken_Calculator.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserNumber { get; set; }
        public double UserMoney { get; set; }
        

    }
    public class UserHistory
    {
        public int ID { get; set; }
        public string UserNumber { get; set; }
        public string History { get; set; }
        public User User { get; set; }
    }

    public class CalculatorDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }

    }
}