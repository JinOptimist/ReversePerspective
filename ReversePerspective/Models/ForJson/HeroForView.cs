using System.Collections.Generic;
using DAO.Model;

namespace ReversePerspective.Models.ForJson
{
    public class HeroForView : BaseModelForView
    {
        public HeroForView(Hero hero)
            : base(hero)
        {
            Name = hero.Name;
            HeroInfo = new List<HeroInfoForView>();
            foreach (var heroInfo in hero.HeroInfo)
            {
                var heroInfoForView = new HeroInfoForView(heroInfo);
                HeroInfo.Add(heroInfoForView);
            }
        }

        public string Name { get; set; }

        public List<HeroInfoForView> HeroInfo { get; set; }
    }
}