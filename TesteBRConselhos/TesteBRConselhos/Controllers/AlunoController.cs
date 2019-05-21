using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteBRConselhos.DAL;
using TesteBRConselhos.Models;

namespace TesteBRConselhos.Controllers
{
    public class AlunoController : Controller
    {
        private SistemaContext db = new SistemaContext();

        // GET: Aluno
        public ActionResult Index()
        {
            var alunos = db.Alunos.Include(a => a.Professor).ToList();

            foreach (var aluno in alunos)
            {
                aluno.idade = GetDifferenceInYears(aluno.DataNascimento);
            }

            return View(alunos.ToList());
        }

        // GET: Aluno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        public int GetDifferenceInYears(DateTime startDate)
        {
            DateTime endDate = DateTime.Now;
            return (endDate.Year - startDate.Year - 1) +
                (((endDate.Month > startDate.Month) ||
                ((endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day))) ? 1 : 0);
        }

        public ActionResult AlunosMaiorDezesseis(int? id)
        {
            var alunos = db.Alunos.Include(a => a.Professor).ToList().Where(x => GetDifferenceInYears(x.DataNascimento) > 16);

            foreach (var aluno in alunos)
            {
                aluno.idade = GetDifferenceInYears(aluno.DataNascimento);
            }

            return View(alunos);
        }

        // GET: Aluno/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorID = new SelectList(db.Professores, "ID", "Nome");
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,DataNascimento,ProfessorID")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {

                if (aluno.DataNascimento.Date >= DateTime.Now.Date)
                {
                    ModelState.AddModelError("", "A data de nascimento deve ser anterior à data atual!");
                    ViewBag.ProfessorID = new SelectList(db.Professores, "ID", "Nome", aluno.ProfessorID);
                    return View(aluno);
                }
                db.Alunos.Add(aluno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professores, "ID", "Nome", aluno.ProfessorID);
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professores, "ID", "Nome", aluno.ProfessorID);
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,DataNascimento,ProfessorID")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                if (aluno.DataNascimento.Date >= DateTime.Now.Date)
                {
                    ModelState.AddModelError("", "A data de nascimento deve ser anterior à data atual!");
                    ViewBag.ProfessorID = new SelectList(db.Professores, "ID", "Nome", aluno.ProfessorID);
                    return View(aluno);
                }

                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professores, "ID", "Nome", aluno.ProfessorID);
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            db.Alunos.Remove(aluno);
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
