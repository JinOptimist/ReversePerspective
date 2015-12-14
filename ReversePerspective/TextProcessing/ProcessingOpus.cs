using System;
using System.Collections.Generic;
using DAO.Model;
using ReversePerspective.Models;

namespace ReversePerspective.TextProcessing
{
    public static class ProcessingOpus
    {
        public static Opus Processing(OpusRaw opusRaw)
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
    }
}