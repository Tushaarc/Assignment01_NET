using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("TblKeyval")]
    public class Keyval
    {
        [Required][Key]   // [Column(TypeName = "CHAR(36)")]  //[Column("KName")]
        public Guid KName { get; set; }
        
    
        [Column("KValue")]
        public string? KValue { get; set; }

    }
}