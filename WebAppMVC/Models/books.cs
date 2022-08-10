
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi5.Model
{
    public class books
    {
        [Key]
        [Required]
        [Column(TypeName = "int")]
        public int bookID { get; set; }
        [Required]
        [Column(TypeName = "Varchar(50)")]
        [MaxLength(50, ErrorMessage = "Book Name should not be more than 50 char")]
        public string BookName { get; set; }
        [Required]
        [Column(TypeName = "Varchar(15)")]
        [MaxLength(15, ErrorMessage = "Zoner Name should not be more than 15 char")]
        public string Zoner { get; set; }

        [Required]


        [Display(Name = "ReleaseDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]


       
        public DateTime ReleaseDate { get; set; }
        public float BookCost { get;set; }

    }
}
