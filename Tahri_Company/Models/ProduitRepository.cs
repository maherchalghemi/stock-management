using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class ProduitRepository
    {
        protected ApplicationDbContext Db = new ApplicationDbContext();

        public bool Add(Produit obj)
        {
            Db.Set<Produit>().Add(obj);
            if (Db.SaveChanges() > 0)
            {
                return true;
            }
            else
                return false;
        }

        public Produit GetById(Int32 id = 0)
        {
            return Db.Set<Produit>().Find(id);
        }



        public IEnumerable<Produit> GetAll()
        {
            return Db.Set<Produit>().ToList();
        }

        public bool Update(Produit obj)
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
                    var obj = Db.Set<Produit>().Find(id);
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