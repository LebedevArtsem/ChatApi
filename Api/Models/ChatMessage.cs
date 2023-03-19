using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("messages")]
    public class ChatMessage
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("sended_person")]
        public string SendedUser { get; set; }

        [Column("recieved_person")]
        public string RecievedUser { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("datetime")]
        public DateTime Time { get; set; }

        [Column("isread")]
        public bool IsRead { get; set; }
    }
}
