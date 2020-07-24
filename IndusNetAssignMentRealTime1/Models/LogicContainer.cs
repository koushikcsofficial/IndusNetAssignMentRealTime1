using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndusNetAssignMentRealTime1.Models
{
    public class LogicContainer
    {
        private DatabaseContext db;
        private UserModel user;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public DateTime CurrentIndianTime()
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            return indianTime;
        }
        public bool IsAdminPresent()
        {
            db = new DatabaseContext();
            if (db.Users.Any(user => user.FirstName.Equals("Admin", StringComparison.OrdinalIgnoreCase) || user.Email.Equals("Admin@site.com", StringComparison.OrdinalIgnoreCase)))
                return true;
            else
                return false;
        }

        public UserModel Register(string userFirstName, string userLastName, DateTime userDob, string userGender, string userEmail, string userPassword)
        {
            db = new DatabaseContext();
            if (db.Users.Any(user => user.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase)) && IsAdminPresent())
            {
                return null;
            } 
            else
            {
                if (!IsAdminPresent() && (userFirstName.Equals("Admin", StringComparison.OrdinalIgnoreCase) || userEmail.Equals("Admin@site.com", StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        user = new UserModel();

                        user.UserId = Guid.NewGuid();
                        user.FirstName = userFirstName.Trim();
                        user.LastName = userLastName.Trim();
                        user.DOB = userDob;
                        user.Gender = userGender.Trim();
                        user.Email = userEmail.Trim();
                        user.Password = userPassword.Trim();
                        user.CreatedDate = CurrentIndianTime();
                        user.CreatedBy = "Admin";

                        db.Users.Add(user);
                        db.SaveChanges();

                        return user;
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                
            }
        }

        public UserModel Login(string userEmail, string userPassword)
        {
            db = new DatabaseContext();
            if (db.Users.Any(user => user.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase) && user.Password == userPassword))
                return db.Users.Where(user => user.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            else
                return null;
        }
    }
}