using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Vault
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public Profile Creator { get; set; }
    }
}