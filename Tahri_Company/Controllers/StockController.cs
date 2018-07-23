using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Tahri_Company.Models;

namespace Tahri_Company.Controllers
{
   public class StockController : Controller
    {
   #region fields
        public DepotRepository _DepotRepository = new DepotRepository();
         public StockDepotRepository _StockDepotRepository = new StockDepotRepository();
         public ProduitRepository _ProduitRepository = new ProduitRepository();
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();

        }
        [HttpGet]
        public JsonResult LoadProd( int page, int pageSize = 3)
        {
           

            var model1 = _ProduitRepository.GetAll();
            model1 = model1.OrderBy(x => x.Libelle);


            


            return Json(new
            {
                status = true,
                droplist = model1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadData(string id,int page, int pageSize = 3 )

        {
            int z = Int32.Parse(id);
            var model = _StockDepotRepository.GetAll().Where(s => s.DepotId == z);

            var model1 = _ProduitRepository.GetAll();
            model1 = model1.OrderBy(x => x.Libelle);


            int totalRow = model.Count();

            model = model.OrderByDescending(x => x.designation)
              .Skip((page - 1) * pageSize)
              .Take(pageSize);


            return Json(new
            {
                data = model,
                total = totalRow,
                status = true,
                droplist = model1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var StockDepot = _StockDepotRepository.GetById(id);
            return Json(new
            {
                data = StockDepot,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveData(string strStock)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StockDepot stockDepot = serializer.Deserialize<StockDepot>(strStock);
            bool status = false;
            string message = string.Empty;
            //add new employee if id = 0
            if (stockDepot.qte == 0) 
            {
                status = false;
            }

            else
            {


                if (stockDepot.Id == 0)
                {

                    try
                    {
                        _StockDepotRepository.Add(stockDepot);
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        status = false;
                        message = ex.Message;
                    }
                }
                else
                {
                    //update existing DB
                    //save db

                    var entity = _StockDepotRepository.GetById(stockDepot.Id);
                    entity.qte = stockDepot.qte;
                    entity.designation = stockDepot.designation;
                    entity.DepotId = stockDepot.DepotId;

                    entity.Id = stockDepot.Id;

                    try
                    {
                        _StockDepotRepository.Update(entity);
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        status = false;
                        message = ex.Message;
                    }

                }
            }

            return Json(new
            {
                status = status,
                message = message
            });
        }




        [HttpPost]
        public JsonResult Delete(int id)
        {


            try
            {
                _StockDepotRepository.Remove(id);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }

       
    }

}