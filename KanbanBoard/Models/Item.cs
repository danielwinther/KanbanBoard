using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KanbanBoard.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        [Required]
        public int Index { get; set; }
        public int ListId { get; set; }
        public virtual List List { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}