using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMgmtWeb.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Department Code")]
        public string Code { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        // Navigation property
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}