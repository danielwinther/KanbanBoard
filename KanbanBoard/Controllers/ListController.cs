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
    public class ListController : Controller
    {
        private readonly DataContext _dataContext = new DataContext();

        public ActionResult Lists(int id)
        {
            ListViewModel vm = new ListViewModel();

            vm.Members = (from m in _dataContext.Members select m).ToList();
            vm.Lists = (from l in _dataContext.Lists where l.BoardId == id select l).ToList();

            return View(vm);
        }

        public ActionResult CreateList()
        {
            ViewBag.BoardId = new SelectList(_dataContext.Boards, "BoardId", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateList([Bind(Include = "ListId,Name,BoardId")] List list)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Lists.Add(list);
                _dataContext.SaveChanges();
                return RedirectToAction("Lists", new { id = list.BoardId });
            }

            ViewBag.BoardId = new SelectList(_dataContext.Boards, "BoardId", "Name", list.BoardId);
            return View(list);
        }
        public ActionResult EditList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = _dataContext.Lists.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = new SelectList(_dataContext.Boards, "BoardId", "Name", list.BoardId);
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditList([Bind(Include = "ListId,Name,BoardId")] List list)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Entry(list).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Lists", new { id = list.BoardId });
            }
            ViewBag.BoardId = new SelectList(_dataContext.Boards, "BoardId", "Name", list.BoardId);
            return View(list);
        }
        
        public ActionResult DeleteList(int id)
        {
            var list = (from l in _dataContext.Lists where l.ListId == id select l).Single();

            _dataContext.Lists.Remove(list);
            _dataContext.SaveChanges();
            return RedirectToAction("Lists", "List", new {id = list.BoardId});
        }
    }
}