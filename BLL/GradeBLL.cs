using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
    public class GradeBLL : IDisposable
    {
        MyContext Context;
      public Grade grade { get; private set; }
    public GradeBLL()
    {
            Context = new MyContext();

    }

    public bool FindById(int GradeId)
        {
            grade = Context.Grade.SingleOrDefault(x => x.GradeId == GradeId);
            return grade != null;
    }

    public List<Grade> GetAll()
    {
        return Context.Grade.ToList();
    }

    public bool Add(Grade grade)
    {
        try
        {
            Context.Grade.Add(grade);
            return Context.SaveChanges() > 0;
        }
        catch
        {

            return false;
        }
    }
    public bool Remove(int GeadeId)
             {
        if (FindById(GeadeId))
        {
                Context.Grade.Remove(grade);
            return Context.SaveChanges() > 0;
        }
        else
            return false;
    }

    public bool Update(Grade grade)
    {
        Context.Entry(grade).State = EntityState.Modified;
        return Context.SaveChanges() > 0;
    }


    public void Dispose()
    {
        Context.Dispose();
    }
}
    
}
