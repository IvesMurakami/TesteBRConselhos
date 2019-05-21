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
    public class ProfessorController : Controller
    {
        private SistemaContext db = new SistemaContext();

        // GET: Professor
        public ActionResult Index()
        {
            return View(db.Professores.ToList());
        }

        public int GetDifferenceInYears(DateTime startDate)
        {
            DateTime endDate = DateTime.Now;
            return (endDate.Year - startDate.Year - 1) +
                (((endDate.Month > startDate.Month) ||
                ((endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day))) ? 1 : 0);
        }

        public ActionResult ProfessoresComAlunosEntreQuinzeEDezessete()
        {

            var professores = db.Professores.Include(a => a.Alunos).ToList();

            List<Professor> professoresList = new List<Professor>();
            List<Aluno> alunosAux; 

            foreach (var professor in professores)
            {
                alunosAux = new List<Aluno>();

                //Verifica cada aluno para filtrar somente aqueles entre idades 15 e 17 anos
                foreach (var aluno in professor.Alunos)
                {
                    aluno.idade = GetDifferenceInYears(aluno.DataNascimento);
                    if (aluno.idade >= 15 && aluno.idade <= 17)
                    {
                        alunosAux.Add(aluno);
                    }
                }

                //Se houver aluno para o professor, adiciona todos os alunos filtrados e o Professor na lista.
                //Caso não exista alunos no filtro, não adiciona o professor
                if (alunosAux.Any())
                {
                    professoresList.Add(new Professor {ID = professor.ID, Nome = professor.Nome, Alunos = alunosAux });
                }
                
            }


            return View(professoresList);


            //return View(db.Professores.ToList());
        }

        public ActionResult AlunosMaiorDezesseis(int? id)
        {
            var professores = db.Professores.Include(a => a.Alunos.Where(x => GetDifferenceInYears(x.DataNascimento)> 16)).ToList();



            return View(professores);
        }

        // GET: Professor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professores.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }


        // GET: Professor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professores.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professor);
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professores.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professores.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professores.Find(id);
            db.Professores.Remove(professor);
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
