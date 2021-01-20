using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Autuas.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(70)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        // public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created_at { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<DailyCheckIn> DailyCheckIns { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
