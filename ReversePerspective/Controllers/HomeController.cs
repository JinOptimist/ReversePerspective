using System.Linq;
using System.Web.Mvc;
using DAO.Model;
using DAO.Repository;
using ReversePerspective.Models;
using ReversePerspective.Models.ForJson;
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
            var result = new OpusForView(opus);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpuses()
        {
            var opuses = _opusRepository.GetAll();

            var result = opuses.Select(opus => new OpusForView(opus)).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
