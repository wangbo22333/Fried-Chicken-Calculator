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
        public CalculatorDBContext() : base("CalculatorDB")
        {
            Database.SetInitializer(new CalculatorDBInitializer());
        }       
        public DbSet<User> Users { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }

    }
    public class CalculatorDBInitializer : CreateDatabaseIfNotExists<CalculatorDBContext>
    {
        protected override void Seed(CalculatorDBContext context)
        {
            IList<User> defusers = new List<User>();
            IList<UserHistory> defuserHistories = new List<UserHistory>();
            defusers.Add(
                new User
                {
                    ID = 1,
                    UserName = "wangbo",
                    UserPassword = "123456",
                    UserNumber = "1001",
                    UserMoney = 9999
                }
                );
            defusers.Add(
                new User
                {
                    ID = 2,
                    UserName = "fangmx",
                    UserPassword = "111111",
                    UserNumber = "1002",
                    UserMoney = 1000
                }
                );
            defusers.Add(
                new User
                {
                    ID = 3,
                    UserName = "wwd123",
                    UserPassword = "222222",
                    UserNumber = "1003",
                    UserMoney = 5000
                }
                );
            context.Users.AddRange(defusers);
            defuserHistories.Add(
                new UserHistory
                {
                    ID = 1,
                    UserNumber = "1001",
                    History = "1+1=2",
                }
                );
            defuserHistories.Add(
                new UserHistory
                {
                    ID = 2,
                    UserNumber = "1001",
                    History = "2+2=4",
                }
                );
            defuserHistories.Add(
                new UserHistory
                {
                    ID = 3,
                    UserNumber = "1001",
                    History = "3-1=2",
                }
                );
            defuserHistories.Add(
                new UserHistory
                {
                    ID = 4,
                    UserNumber = "1001",
                    History = "10*10=100",
                }
                );
            context.UserHistories.AddRange(defuserHistories);
            base.Seed(context);
        }
        
    }

}