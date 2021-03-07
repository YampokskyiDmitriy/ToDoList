using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    /// <summary>
    /// ToDoes Controller
    /// </summary>
    public class ToDoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoes
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<ToDo> GetToDoes()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            IEnumerable<ToDo> myToDoes = db.ToDos.ToList().Where(x => x.User == currentUser);

            int completeCount = 0;
            foreach (ToDo toDo in myToDoes)
            {
                if (toDo.IsDone)
                {
                    completeCount++;
                }
            }
            //Check of percentage of progress
            //If percent is not a number then percentage equals zero
            int num;
            ViewBag.Percent = Math.Round(100f * ((float)completeCount / (float)myToDoes.Count()));
            bool isNum = int.TryParse(ViewBag.Percent.ToString(), out num);
            if (!isNum)
                ViewBag.Percent = 0;

            return myToDoes;
        }

        public ActionResult BuildToDoTable()
        {
            return PartialView("_ToDoTable", GetToDoes());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Description")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                toDo.User = currentUser;
                toDo.IsDone = false;
                db.ToDos.Add(toDo);
                db.SaveChanges();
            }
            return PartialView("_ToDoTable", GetToDoes());
        }

        [HttpPost]
        public ActionResult AJAXEditIsDoneCheckBox(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            else
            {
                toDo.IsDone = value;
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ToDoTable", GetToDoes());
            }
        }

        // GET: ToDoes/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            db.ToDos.Remove(toDo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}