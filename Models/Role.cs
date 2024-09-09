using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Models
{
    public class Role
    {
        [Key]
        public int RoleId {get; set;}
        [DisplayName("Role Name")]
        public string RoleName {get; set;}
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

    }
}