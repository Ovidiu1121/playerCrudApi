using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerCrudApi.Players.Model
{
    [Table("player")]
    public class Player
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("number")]
        public int Number { get; set; }

        [Required]
        [Column("value")]
        public int Value { get; set; }

    }
}
