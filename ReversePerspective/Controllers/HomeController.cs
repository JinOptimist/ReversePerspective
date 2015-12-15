﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAO;
using DAO.Model;
using DAO.Repository;
using Microsoft.Ajax.Utilities;
using ReversePerspective.Helper;
using ReversePerspective.Models;
using ReversePerspective.Models.ForJson;

namespace ReversePerspective.Controllers
{
    public class HomeController : Controller
    {
        private readonly BaseRepository _baseRepository;
        private readonly HeroRepository _heroRepository;
        private readonly HeroInfoRepository _heroInfoRepository;
        private readonly PhraseRepository _phraseRepository;

        public HomeController()
        {
            var reversePerspectiveContext = new ReversePerspectiveContext();
            _baseRepository = new BaseRepository(reversePerspectiveContext);
            _heroInfoRepository = new HeroInfoRepository(reversePerspectiveContext);
            _heroRepository = new HeroRepository(reversePerspectiveContext);
            _phraseRepository = new PhraseRepository(reversePerspectiveContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteOpus(long opusId)
        {
            _baseRepository.Remove(opusId);
            return RedirectToAction("Index");
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

            _baseRepository.Save(opus);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddHeroInfo(string info, long heroId, long phraseId)
        {
            var phrase = _phraseRepository.Get(phraseId);
            var hero = _heroRepository.Get(heroId);
            var heroAdditionalInfo = new HeroAdditionalInfo
            {
                Info = info,
                Hero = hero,
                VisibleAfterThatPhrase = phrase
            };

            _heroInfoRepository.Save(heroAdditionalInfo);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpus(long id)
        {
            var opus = _baseRepository.Get(id);
            var result = new OpusForView(opus);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpuses()
        {
            var opuses = _baseRepository.GetAll();

            var result = opuses.Select(opus => new OpusForView(opus)).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHeroInfo(long heroId, long phraseId)
        {
            var allHeroInfo = _heroInfoRepository.GetByHero(heroId);

            var correctInfo = allHeroInfo.Where(x => x.VisibleAfterThatPhrase.Id <= phraseId);

            var result =
                correctInfo.Select(heroAdditionalInfo => new HeroInfoForView(heroAdditionalInfo))
                    .OrderByDescending(x => x.VisibleAfterThatParagraphId).ToList();
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
