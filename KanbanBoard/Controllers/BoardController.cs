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
    public class BoardController : Controller
    {
        private readonly DataContext _dataContext = new DataContext();
        public ActionResult Index()
        {
            var boards = (from b in _dataContext.Members.Include("Boards") where b.Username == "Daniel" select b).ToList();

            return View(boards);
        }

        public ActionResult Favourites()
        {
            var favourites = (from f in _dataContext.Favourites
                              join m in _dataContext.Members on f.Member.MemberId equals m.MemberId
                              join b in _dataContext.Boards on f.Board.BoardId equals b.BoardId
                              where m.Username == "Daniel" select f).ToList();
                              
            return View(favourites);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoardId,Name")] Board board)
        {
            if (ModelState.IsValid)
            {
                var member = (from m in _dataContext.Members where m.Username == "Daniel" select m).Single();
                _dataContext.Boards.Add(board);
                member.Boards.Add(board);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(board);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = _dataContext.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoardId,Name")] Board board)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Entry(board).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(board);
        }

        public ActionResult Delete(int id)
        {
            Board board = _dataContext.Boards.Find(id);
            _dataContext.Boards.Remove(board);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Favourite(int id)
        {
            var member = (from m in _dataContext.Members where m.Username == "Daniel" select m).Single();
            
            _dataContext.Favourites.Add(new Favourite() { BoardId = id, MemberId = member.MemberId });
            _dataContext.SaveChanges();
            return RedirectToAction("Favourites");
        }

        public ActionResult Unfavourite(int id)
        {
            var favourite = (from f in _dataContext.Favourites where f.BoardId == id select f).Single();

            _dataContext.Favourites.Remove(favourite);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}