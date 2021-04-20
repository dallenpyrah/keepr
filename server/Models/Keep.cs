using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Keep
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Img { get; set; }

        public int Views { get; set; }

        public int Shares { get; set; }

        public int Keeps { get; set; }

        public string Tags { get; set; }

        public Profile Creator { get; set; }
    }

        public class VaultKeepViewModel : Keep
    {
        public int VaultKeepId { get; set; }
    }
}