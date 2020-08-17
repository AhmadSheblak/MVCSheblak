using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
    public class RoleBLL:IDisposable
    {
        MyContext _context;
        public Role _Roles { get; private set; }
        public RoleBLL()
        {
            _context = new MyContext();

        }

        public bool FindById(int Roleid)
        {
            _Roles = _context.Role.SingleOrDefault(x => x.RoleId == Roleid);
            return _Roles != null;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Role;
        }

        public bool Add(Role _Roles)
        {
            try
            {
                _context.Role.Add(_Roles);
                return _context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public bool Remove(int Roleid)
        {
            if (FindById(Roleid))
            {
                _context.Role.Remove(_Roles);
                return _context.SaveChanges() > 0;
            }
            else
                return false;
        }

        public bool Update(Role _Roles)
        {
            _context.Entry(_Roles).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
