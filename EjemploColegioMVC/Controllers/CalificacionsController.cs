using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EjemploColegioMVC.Models;

namespace EjemploColegioMVC.Controllers
{
    [Authorize]
    public class CalificacionsController : Controller
    {
        private ColegioModelContainer db = new ColegioModelContainer();

        // GET: Calificacions
        public ActionResult Index()
        {
            var calificaciones = db.Calificaciones.Include(c => c.Estudiante).Include(c => c.Asignatura);
            return View(calificaciones.ToList());
        }

        // GET: Calificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificaciones.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        [Authorize(Roles ="Admin, Docente")]
        // GET: Calificacions/Create
        public ActionResult Create()
        {
            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "Id", "Nombre");
            ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "Id", "Codigo");
            return View();
        }

        // POST: Calificacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EstudianteId,AsignaturaId,Acumulado,Examen")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Calificaciones.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "Id", "Nombre", calificacion.EstudianteId);
            ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "Id", "Codigo", calificacion.AsignaturaId);
            return View(calificacion);
        }

        [Authorize(Roles = "Admin, Docente")]
        // GET: Calificacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificaciones.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "Id", "Nombre", calificacion.EstudianteId);
            ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "Id", "Codigo", calificacion.AsignaturaId);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EstudianteId,AsignaturaId,Acumulado,Examen")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstudianteId = new SelectList(db.Estudiantes, "Id", "Nombre", calificacion.EstudianteId);
            ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "Id", "Codigo", calificacion.AsignaturaId);
            return View(calificacion);
        }
        [Authorize(Roles = "Admin, Docente")]
        // GET: Calificacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificaciones.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calificacion calificacion = db.Calificaciones.Find(id);
            db.Calificaciones.Remove(calificacion);
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
