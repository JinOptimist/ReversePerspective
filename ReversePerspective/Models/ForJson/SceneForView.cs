using System.Collections.Generic;
using DAO.Model;

namespace ReversePerspective.Models.ForJson
{
    public class SceneForView : BaseModelForView
    {
        public SceneForView(Scene scene)
            : base(scene)
        {
            Description = scene.Description;
            Phrases = new List<PhraseForView>();

            foreach (var phrase in scene.Phrases)
            {
                var phraseForView = new PhraseForView(phrase);
                Phrases.Add(phraseForView);
            }
        }

        public string Description { get; set; }

        public List<PhraseForView> Phrases { get; set; }
    }
}