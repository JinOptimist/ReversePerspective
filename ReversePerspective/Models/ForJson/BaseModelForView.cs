using DAO.Model;

namespace ReversePerspective.Models.ForJson
{
    public class BaseModelForView
    {
        public BaseModelForView(BaseModel baseModel)
        {
            Id = baseModel.Id;
        }

        public long Id { get; set; }
    }
}