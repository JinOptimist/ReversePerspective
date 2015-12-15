using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Model;
using ReversePerspective.Models;

namespace ReversePerspective.TextProcessing
{
    public static class ProcessingOpus
    {
        private const string Pattern = @" [A-ZА-Я][\wа-я]*";

        public static Opus RawToOpus(OpusRaw opusRaw)
        {
            var opus = new Opus
            {
                Name = opusRaw.Name,
                Scenes = new List<Scene>()
            };

            var scene = new Scene
            {
                Phrases = new List<Phrase>()
            };
            opus.Scenes.Add(scene);
            opus.Heroes = new List<Hero>();

            var index = 0;
            var lines = opusRaw.AllText.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var textPosition = line.IndexOf(":", StringComparison.Ordinal);
                if (textPosition < 0)
                {
                    scene.Description = line;
                    continue;
                }
                var heroName = line.Substring(0, textPosition);
                var text = line.Substring(textPosition + 1);

                var hero = opus.Heroes.SingleOrDefault(x => x.Name == heroName);
                if (hero == null)
                {
                    hero = new Hero
                    {
                        Scene = scene,
                        Name = heroName
                    };
                    opus.Heroes.Add(hero);
                }

                var phrase = new Phrase
                {
                    Position = ++index,
                    Text = text,
                    Scene = scene,
                    Hero = hero
                };
                scene.Phrases.Add(phrase);
            }

            return opus;
        }

        public static string GetTextFromOpus(Opus opus)
        {
            var sb = new StringBuilder();

            foreach (var scene in opus.Scenes)
            {
                foreach (var phrase in scene.Phrases)
                {
                    sb.AppendLine(phrase.Text);
                }
            }

            return sb.ToString();
        }
    }
}
