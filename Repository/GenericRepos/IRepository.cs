using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepos
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Insert(TEntity newEntity);
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        //expressionbe rakjuk a lambda kifejezést: fordítva tárolódik el, és jobban tud a gép optimalizálni

        //List<int> szamok=xxx;
        //ienumerable
        //szamok.Where(x=>x%2==0).Where(x=>x%3==0).OrderBy(x=>x).Reverse();
        //ez itt négy egymás utáni szerver művelet
        //iqueryable
        //var result=DBCONTEXT.TABLA.Where(x=>x%2==0).Where(x=>x%3==0).OrderBy(x=>x).Reverse();
        //foreach (var akt in result){}
        //itt optimalizálja a kódot és egy lekérdezés megy ki a szervernek
    }
}
