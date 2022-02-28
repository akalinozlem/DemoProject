using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Models
{
    public class AdminUser
    {
        /// <summary>
        /// Admin User Id.
        /// </summary>
        [Required]
        [Column("UserId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /// <summary>
        /// Admin Username.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        [Column("UserName")]
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// Admin User email address.
        /// </summary>
        [Display(Name = "Email")]
        [Column("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Admin User password.
        /// </summary>
        [Required]
        [Display(Name = "Password")]
        [Column("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
