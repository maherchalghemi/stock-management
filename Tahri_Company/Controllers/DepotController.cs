using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Tahri_Company.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
namespace Tahri_Company.Controllers
{
    public class DepotController : Controller
    {
        #region fields
        public DepotRepository _DepotRepository = new DepotRepository();
         public StockDepotRepository _StockDepotRepository = new StockDepotRepository();
         public ProduitRepository _ProduitRepository = new ProduitRepository();
        #endregion
         // GET: Depot
         #region methods

         public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            return View(); ;

        }

        

        [HttpGet]
        public JsonResult LoadData(int page, int pageSize = 3)
        {
            var model = _DepotRepository.GetAll();
            var model1 = _ProduitRepository.GetAll();
            model1 = model1.OrderBy(x => x.Libelle);
            var userId = User.Identity.GetUserId();


            int totalRow = model.Count();

            model = model.OrderByDescending(x => x.Nom)
              .Skip((page - 1) * pageSize)
              .Take(pageSize);


            return Json(new
            {
                data = model,
                droplist = model1,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDetail(int id)
        {
            var depot = _DepotRepository.GetById(id);
            return Json(new
            {
                data = depot,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveData(string strDepot)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Depot depot = serializer.Deserialize<Depot>(strDepot);
            bool status = false;
            string message = string.Empty;
            //add new employee if id = 0
            if ((depot.Nom == null) )
            {
                status = false;
            }

            else
            {


                if (depot.DepotId == 0)
                {

                    try
                    {
                        _DepotRepository.Add(depot);
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

                    var entity = _DepotRepository.GetById(depot.DepotId);
                    entity.Nom = depot.Nom;
                    entity.Adresse = depot.Adresse;
                    entity.Tel = depot.Tel;
                    

                    entity.DepotId = depot.DepotId;

                    try
                    {
                        _DepotRepository.Update(entity);
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

        public JsonResult UpdateData(string strDepot)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Depot depot = serializer.Deserialize<Depot>(strDepot);
            bool status = false;
            string message = string.Empty;

            if ((depot.Nom == null))
            {
                status = false;
            }

            else
            {



                var entity = _DepotRepository.GetById(depot.DepotId);
                entity.Nom = depot.Nom;
                entity.Adresse = depot.Adresse;
                entity.Tel = depot.Tel;


                entity.DepotId = depot.DepotId;

                try
                {
                    _DepotRepository.Update(entity);
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }


            }

            return Json(new
            {
                status = status,
                message = message
            });
        }


        public JsonResult SaveProd(string strStock)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StockDepot stockDepot = serializer.Deserialize<StockDepot>(strStock);
            stockDepot.DepotId = _DepotRepository.GetMax();
             
            bool status = false;
            string message = string.Empty;

            if ((stockDepot.qte == 0))
            {
                status = false;
            }

            else
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
                _DepotRepository.Remove(id);
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

         #endregion

    }
}