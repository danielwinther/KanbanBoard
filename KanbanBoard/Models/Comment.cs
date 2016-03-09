using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KanbanBoard.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}