using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HAMDA.Models.EntityModels.Base
{
    public class EntityBase
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "int")]
        public int Status { get; set; } = (int)Enum.Status.Active;
        public Guid? ModifiedById { get; set; }

    }
}
