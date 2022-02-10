
using Infrastructure.Attributes;

namespace Infrastructure.Model {
    [Table("Users")]
    public class UserModel : BaseModel {
        [Column("UserName")]
        public string Name { get; set; }
        public string Pwd { get; set; }
    }




 public class UserModelDTO : BaseModel {
        [Column("UserName")]
        public string Name { get; set; }
        public string Pwd { get; set; }
    }

}