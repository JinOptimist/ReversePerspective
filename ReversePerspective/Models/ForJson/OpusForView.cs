using System.Collections.Generic;
using DAO.Model;

namespace ReversePerspective.Models.ForJson
{
    public class OpusForView : BaseModelForView
    {
        public OpusForView(Opus opus)
            : base(opus)
        {
            Name = opus.Name;
            Scenes = new List<SceneForView>();
            Heroes = new List<HeroForView>();

            foreach (var scene in opus.Scenes)
            {
                var sceneForView = new SceneForView(scene);
                Scenes.Add(sceneForView);
            }

            foreach (var hero in opus.Heroes)
            {
                var heroForView = new HeroForView(hero);
                Heroes.Add(heroForView);
            }
        }

        public string Name { get; set; }

        public List<SceneForView> Scenes { get; set; }

        public List<HeroForView> Heroes { get; set; }
    }
}