using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class StockDepotRepository
    {
        protected ApplicationDbContext Db = new ApplicationDbContext();

        public bool Add(StockDepot obj)
        {
            Db.Set<StockDepot>().Add(obj);
            if (Db.SaveChanges() > 0)
            {
                return true;
            }
            else
                return false;
        }

        public StockDepot GetById(Int32 id = 0)
        {
            return Db.Set<StockDepot>().Find(id);
        }



        public IEnumerable<StockDepot> GetAll()
        {
            return Db.Set<StockDepot>().ToList();
        }

        public bool Update(StockDepot obj)
        {

            //var obj = Db.Set<TEntity>().Find(id);
            // Db.Entry(obj).State = EntityState.Modified;
            //if (Db.SaveChanges() > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            try
            {
                using (var dbCtx = new ApplicationDbContext())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool Remove(int id)
        {
            //var obj = Db.Set<TEntity>().Find(id);
            ////Db.Entry(obj2).State = EntityState.Deleted;
            //Db.Set<TEntity>().Remove(obj);
            //if (Db.SaveChanges() > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            try
            {
                using (var dbCtx = new ApplicationDbContext())
                {
                    var obj = Db.Set<StockDepot>().Find(id);
                    //3. Mark entity as modified
                    dbCtx.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}