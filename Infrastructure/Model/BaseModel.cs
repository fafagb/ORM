using Infrastructure.Attributes.Filter;
namespace Infrastructure.Model
{
    public class BaseModel
    {
        [Filter]
        public int Id { get; set; }
    }
}