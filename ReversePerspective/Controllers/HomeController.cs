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
        //
        // GET: /Home/
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
                Id = opus.Id
            };

            return View("AddOpus", opusRaw);
        }

        [HttpPost]
        public ActionResult AddOpus(OpusRaw opusRaw)
        {
            var opus = ProcessingOpus.Processing(opusRaw);
            _opusRepository.Save(opus);

            return RedirectToAction("Index");
        }

        public JsonResult GetOpus(long id)
        {
            var result = Opuses();
            var opus = result.Where(x => x.Id == id);
            return Json(opus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpuses()
        {
            var result = _opusRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<Opus> Opuses()
        {
            var paragraphs = new List<Paragraph>
            {
                new Paragraph
                {
                    Position = 1,
                    Text = "3 qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe"
                },
                new Paragraph
                {
                    Position = 2,
                    Text = "1"
                },
                new Paragraph
                {
                    Position = 3,
                    Text = "2 qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe qwe"
                }
            };

            var result = new List<Opus>();
            var opus = new Opus
            {
                Id = 1,
                Name = "O1",
                Paragraphs = paragraphs
            };
            result.Add(opus);

            opus = new Opus
            {
                Id = 2,
                Name = "New O2 Do",
                Paragraphs = paragraphs
            };
            result.Add(opus);
            return result;
        }
    }
}
