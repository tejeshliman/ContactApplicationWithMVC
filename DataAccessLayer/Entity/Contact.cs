namespace DataAccessLayer
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Contact")]
    public class Contact
    {
        public int ID { get; set; }

        [Required()]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string PrimaryEmail { get; set; }

        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
