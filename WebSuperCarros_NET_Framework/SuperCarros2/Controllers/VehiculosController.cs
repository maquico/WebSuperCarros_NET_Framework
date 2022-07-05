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
    public class VehiculosController : Controller
    {
        private DSII_SuperCarrosEntities2 db = new DSII_SuperCarrosEntities2();

        // GET: Vehiculos
        public ActionResult Index()
        {
            var vehiculos = db.Vehiculos.Include(v => v.Brand).Include(v => v.Color).Include(v => v.Model).Include(v => v.Type);
            return View(vehiculos.ToList());
        }
        public ActionResult ImgView()
        {
            var vehiculos = db.Vehiculos.Include(v => v.Brand).Include(v => v.Color).Include(v => v.Model).Include(v => v.Type);
            return View(vehiculos.ToList());
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name");
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Name");
            ViewBag.ModeI_Id = new SelectList(db.Model, "Id", "Name");
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name");
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand_Id,ModeI_Id,Color_Id,Precio,Combustible,Type_Id,Puertas,Pasajeros,Motor,Transmisión,Año,Imagen,CreatedDate")] Vehiculos vehiculos, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                vehiculos.Id = Guid.NewGuid().ToString();
                vehiculos.CreatedDate = DateTime.Now;
                if(imgUpload != null)
                {
                    string imgUrl = System.IO.Path.GetFileName(imgUpload.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/public/vehiculos_img"), imgUrl);

                    imgUpload.SaveAs(pathUrl);
                    vehiculos.Imagen = imgUrl;
                }

                db.Vehiculos.Add(vehiculos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name", vehiculos.Brand_Id);
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Name", vehiculos.Color_Id);
            ViewBag.ModeI_Id = new SelectList(db.Model, "Id", "Name", vehiculos.ModeI_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", vehiculos.Type_Id);
            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name", vehiculos.Brand_Id);
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Name", vehiculos.Color_Id);
            ViewBag.ModeI_Id = new SelectList(db.Model, "Id", "Name", vehiculos.ModeI_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", vehiculos.Type_Id);
            return View(vehiculos);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand_Id,ModeI_Id,Color_Id,Precio,Combustible,Type_Id,Puertas,Pasajeros,Motor,Transmisión,Año,Imagen,CreatedDate")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Brand_Id = new SelectList(db.Brand, "Id", "Name", vehiculos.Brand_Id);
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Name", vehiculos.Color_Id);
            ViewBag.ModeI_Id = new SelectList(db.Model, "Id", "Name", vehiculos.ModeI_Id);
            ViewBag.Type_Id = new SelectList(db.Type, "Id", "Name", vehiculos.Type_Id);
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculos);
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
