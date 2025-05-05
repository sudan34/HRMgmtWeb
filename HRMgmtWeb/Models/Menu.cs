using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMgmtWeb.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string IconClass { get; set; } = "bi bi-circle";

        [StringLength(100)]
        public string Url { get; set; }

        public int? ParentId { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(50)]
        public string RoleRequired { get; set; } // For role-based menu items

        [ForeignKey("ParentId")]
        public virtual Menu Parent { get; set; }

        public virtual ICollection<Menu> Children { get; set; }
    }
}