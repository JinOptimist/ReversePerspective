using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAO.Model;
using DAO.Repository;
using ReversePerspective.Models;
using ReversePerspective.TextProcessing;

namespace ReversePerspective.Controllers
{
    public class HomeController : Controller
    {
        private readonly OpusRepository _opusRepository;
        
        public HomeController()
        {
            _opusRepository = new OpusRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddOpus()
        {
            return View();
        }

        public ActionResult UpdateOpus(Opus opus)
        {
            var opusRaw = new OpusRaw
            {
                Id = opus.Id,
                AllText = ProcessingOpus.GetTextFromOpus(opus)
            };

            return View("AddOpus", opusRaw);
        }

        [HttpPost]
        public ActionResult AddOpus(OpusRaw opusRaw)
        {
            var opus = ProcessingOpus.RawToOpus(opusRaw);
            
            _opusRepository.Save(opus);

            return RedirectToAction("Index");
        }

        public JsonResult GetOpus(long id)
        {
            var opus = _opusRepository.Get(id);
            return Json(opus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpuses()
        {
            var result = _opusRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
