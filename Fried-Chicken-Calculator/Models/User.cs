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
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}