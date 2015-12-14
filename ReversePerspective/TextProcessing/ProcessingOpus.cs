using System;
using System.Collections.Generic;
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
                Paragraphs = new List<Paragraph>()
            };

            var index = 0;
            var lines = opusRaw.AllText.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var paragraph = new Paragraph
                {
                    Guid = new Guid(),
                    Position = ++index,
                    Text = line
                };
                opus.Paragraphs.Add(paragraph);
            }

            return opus;
        }

        public static string GetTextFromOpus(Opus opus)
        {
            var sb = new StringBuilder();

            foreach (var paragraph in opus.Paragraphs)
            {
                sb.AppendLine(paragraph.Text);
            }

            return sb.ToString();
        }
    }
}
