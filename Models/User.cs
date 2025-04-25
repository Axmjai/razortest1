using System.ComponentModel.DataAnnotations;

namespace CBEs.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
