using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("friends")]
    public class Friend
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_email")] 
        public string UserEmail { get; set; }

        [ForeignKey("UserEmail")]
        public User Users { get; set; }

        [Column("friend_email")]
        public string FriendEmail { get; set; }

        [ForeignKey("FriendEmail")]
        public User Friends { get; set; }
    }
}
