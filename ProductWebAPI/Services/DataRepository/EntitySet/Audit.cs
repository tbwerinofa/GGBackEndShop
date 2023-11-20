using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DataRepository
{
    public class Audit
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.DateTime CreatedTimestamp { get; set; }
        public Nullable<System.DateTime> UpdatedTimestamp { get; set; }

        //[Required]
        //public string UserId { get; set; }

    }
}
