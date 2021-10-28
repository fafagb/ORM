using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Model {
    [Table("User")]
    public class UserModel : BaseModel {
        [Column("UserName")]
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
}