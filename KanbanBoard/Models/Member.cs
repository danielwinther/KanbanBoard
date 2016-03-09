using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KanbanBoard.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(150)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}