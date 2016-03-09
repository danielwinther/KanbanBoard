using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KanbanBoard.Models
{
    public class Favourite
    {
        [Key]
        [Column(Order = 1)]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        [Key]
        [Column(Order = 2)]
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
    }
}