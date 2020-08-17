using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
    public class UserBLL : IDisposable
    {
        MyContext Context;
        public User user { get; private set; }
        public UserBLL()
        {
            Context = new MyContext();
        }
        public bool FindById(string username)
        {
         user=Context.User.SingleOrDefault(x => x.User_name == username);
            return user != null;
        }
        public List<User> GetAll()
        {
            return Context.User.ToList();
        }
        public bool Add(User user)
        {
            try
            {
                Context.User.Add(user);
                return Context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool Remove(string username)
        {
            if (FindById(username))
            {
                Context.User.Remove(user);
                return Context.SaveChanges() > 0;
            }
            return false;
        }
        public bool Update(User user)
        {
            Context.Entry(user).State = EntityState.Modified;
            return Context.SaveChanges() > 0;
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
