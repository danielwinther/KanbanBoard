using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KanbanBoard.Models;

namespace KanbanBoard.Models
{
    public class ListViewModel
    {
        public List<Member> Members { get; set; }
        public List<List> Lists { get; set; }
    }
}