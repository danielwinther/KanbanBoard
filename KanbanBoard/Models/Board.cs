using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace KanbanBoard.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public virtual ICollection<List> Lists { get; set; }
        public virtual ICollection<Member> Members { get; set; }

    }
}