using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KanbanBoard.Models
{
    public class List
    {
        public int ListId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}