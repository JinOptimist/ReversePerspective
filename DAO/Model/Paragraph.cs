using System;

namespace DAO.Model
{
    public class Paragraph : BaseModel
    {
        public Guid Guid { get; set; }

        public long Position { get; set; }

        public string Text { get; set; }
    }
}