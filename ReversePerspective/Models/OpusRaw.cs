using System;

namespace ReversePerspective.Models
{
    public class OpusRaw
    {
        public long Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string AllText { get; set; }
    }
}