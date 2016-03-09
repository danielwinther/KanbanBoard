using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KanbanBoard.Context;
using KanbanBoard.Models;

namespace KanbanBoard.Controllers
{
    public class ItemController : Controller
    {
        private readonly DataContext _dataContext = new DataContext();

        public ActionResult Item(int id)
        {
            var item =
                (from i in _dataContext.Items.Include("Members").Include("Comments") where i.ItemId == id select i)
                    .Single();

            return View(item);
        }

        public ActionResult Move(string move, int id)
        {
            var item = (from i in _dataContext.Items where i.ItemId == id select i).Single();

            if (move == "Up")
            {
                var movingUp = (from i in _dataContext.Items
                                where i.Index == item.Index && i.ListId == item.ListId
                                select i).Single();
                var movingDown = (from i in _dataContext.Items
                                  where i.Index == item.Index - 1 && i.ListId == item.ListId
                                  select i).Single();

                movingUp.Index = item.Index - 1;
                movingDown.Index = item.Index + 1;
            }
            else
            {
                var movingDown = (from i in _dataContext.Items
                                  where i.Index == item.Index && i.ListId == item.ListId
                                  select i).Single();
                var movingUp = (from i in _dataContext.Items
                                where i.Index == item.Index + 1 && i.ListId == item.ListId
                                select i).Single();

                movingDown.Index = item.Index + 1;
                movingUp.Index = item.Index - 1;
            }
            _dataContext.SaveChanges();
            var board = (from i in _dataContext.Items
                         join l in _dataContext.Lists on i.ListId equals l.ListId
                         where i.ItemId == item.ItemId && i.ListId == item.ListId
                         select l).Single();

            return RedirectToAction("Lists", "List", new { id = board.BoardId });
        }

        public ActionResult DeleteItem(int id)
        {
            var item = (from i in _dataContext.Items where i.ItemId == id select i).Single();

            _dataContext.Items.Remove(item);

            var itemsMoveDown = from i in _dataContext.Items
                                where i.Index > item.Index && i.ListId == item.ListId
                                select i;
            foreach (var itemMoveUp in itemsMoveDown)
            {
                itemMoveUp.Index = itemMoveUp.Index - 1;
            }

            var board = (from i in _dataContext.Items
                         join l in _dataContext.Lists on i.ListId equals l.ListId
                         where i.ItemId == item.ItemId && i.ListId == item.ListId
                         select l).Single();

            _dataContext.SaveChanges();

            return RedirectToAction("Lists", "List", new { id = board.BoardId });
        }

        public ActionResult CreateItem()
        {
            ViewBag.ColorId = new SelectList(_dataContext.Colors, "ColorId", "Name");
            ViewBag.ListId = new SelectList(_dataContext.Lists, "ListId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem([Bind(Include = "ItemId,Title,Description,Deadline,ColorId,Index,ListId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Items.Add(item);
                _dataContext.SaveChanges();
                return RedirectToAction("Item", new { id = item.ItemId });
            }

            ViewBag.ColorId = new SelectList(_dataContext.Colors, "ColorId", "Name", item.ColorId);
            ViewBag.ListId = new SelectList(_dataContext.Lists, "ListId", "Name", item.ListId);
            return View(item);
        }

        public ActionResult EditItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _dataContext.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.ColorId = new SelectList(_dataContext.Colors, "ColorId", "Name", item.ColorId);
            ViewBag.ListId = new SelectList(_dataContext.Lists, "ListId", "Name", item.ListId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem([Bind(Include = "ItemId,Title,Description,Deadline,ColorId,Index,ListId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Entry(item).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Item", new { id = item.ItemId });
            }

            ViewBag.ColorId = new SelectList(_dataContext.Colors, "ColorId", "Name", item.ColorId);
            ViewBag.ListId = new SelectList(_dataContext.Lists, "ListId", "Name", item.ListId);
            return View(item);
        }
        public ActionResult CreateComment()
        {
            ViewBag.ItemId = new SelectList(_dataContext.Items, "ItemId", "Title");
            ViewBag.MemberId = new SelectList(_dataContext.Members, "MemberId", "Username");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentId,Title,Text,ItemId,MemberId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Comments.Add(comment);
                _dataContext.SaveChanges();
                return RedirectToAction("Item", new { id = comment.ItemId });
            }

            ViewBag.ItemId = new SelectList(_dataContext.Items, "ItemId", "Title", comment.ItemId);
            ViewBag.MemberId = new SelectList(_dataContext.Members, "MemberId", "Username", comment.MemberId);
            return View(comment);
        }
        public ActionResult DeleteComment(int id)
        {
            Comment comment = _dataContext.Comments.Find(id);
            _dataContext.Comments.Remove(comment);
            _dataContext.SaveChanges();
            return RedirectToAction("Item", new { id = comment.ItemId });
        }

        public ActionResult RemoveMember(int memberId, int itemId)
        {
            var member = (from m in _dataContext.Members where m.MemberId == memberId select m).Single();
            var item = (from i in _dataContext.Items where i.ItemId == itemId select i).Single();

            item.Members.Remove(member);
            _dataContext.SaveChanges();
            return RedirectToAction("Item", new { id = item.ItemId });
        }

        public ActionResult AddMember()
        {
            ViewBag.MemberId = new SelectList(_dataContext.Members, "MemberId", "Username");
            ViewBag.ItemId = new SelectList(_dataContext.Items, "ItemId", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember([Bind(Include = "MemberId, ItemId")] Member member, Item item)
        {
            var selectedMember = (from m in _dataContext.Members where m.MemberId == member.MemberId select m).Single();
            var selectedItem = (from i in _dataContext.Items where i.ItemId == item.ItemId select i).Single();

            selectedItem.Members.Add(selectedMember);
            _dataContext.SaveChanges();
            return RedirectToAction("Item", new { id = item.ItemId });
        }
    }
}