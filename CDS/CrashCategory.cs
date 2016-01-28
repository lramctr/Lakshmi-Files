using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Nps.Cds.DataModels.NpsCds
{
    [Table("CRASH_CATEGORIES")]
    public partial class CrashCategory
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CrashCategory()
        {
            Crashes = new HashSet<Crash>();
        }

        [Column("OBJECTID")]
        public int ObjectId { get; set; }

        [Key]
        [Column("CATEGORY_CODE")]
        [StringLength(7)]
        [Display(Name = "Crash Category")]
        public string CategoryCode { get; set; }

        [Required]
        [Column("CATEGORY")]
        [StringLength(75)]
        public string Category { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Crash> Crashes { get; set; }
    }
}
