using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SuperCarros2;

namespace SuperCarros2.Controllers
{
    public class ModelosController : Controller
    {
        private DSII_SuperCarrosEntities2 db = new DSII_SuperCarrosEntities2();

        // GET: Modelos
        public ActionResult Index()
        {
            var model = db.Model.Include(m => m.Brand).Include(m => m.Type);
            return View(model.ToList());
        }

        // GET: Modelos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Model.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Modelos/Create
        public ActionResult Create()
        {

            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name");
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name");
            return View();
        }

        // POST: Modelos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Visible,CreatedDate,Brand_Id,Type_Id")] Model model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid().ToString();
                model.CreatedDate = DateTime.Now;
                db.Model.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name", model.Brand_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", model.Type_Id);
            return View(model);
        }

        // GET: Modelos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Model.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name", model.Brand_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", model.Type_Id);
            return View(model);
        }

        // POST: Modelos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Visible,CreatedDate,Brand_Id,Type_Id")] Model model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name", model.Brand_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", model.Type_Id);
            return View(model);
        }

        // GET: Modelos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Model.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Modelos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Model model = db.Model.Find(id);
            db.Model.Remove(model);
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
