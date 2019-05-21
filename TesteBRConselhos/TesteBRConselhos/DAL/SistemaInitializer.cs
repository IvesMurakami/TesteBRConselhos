using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteBRConselhos.Models;

namespace TesteBRConselhos.DAL
{
    public class SistemaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SistemaContext>
    {
        protected override void Seed(SistemaContext context)
        {
            var professor = new List<Professor>
            {
                new Professor{Nome="Alberto Stein"},
                new Professor{Nome="Alano Turing"},
                new Professor{Nome="Tereza Lee"}
            };
            professor.ForEach(s => context.Professores.Add(s));
            context.SaveChanges();


            var alunos = new List<Aluno>
            {
                new Aluno{Nome="João dos Santos",DataNascimento=DateTime.Parse("2005-01-05"),ProfessorID=1},
                new Aluno{Nome="Maria da Silva",DataNascimento=DateTime.Parse("1990-05-08"),ProfessorID =2},
                new Aluno{Nome="Aluno genérico 1",DataNascimento=DateTime.Parse("2000-07-20"),ProfessorID = 1},
                new Aluno{Nome="Aluno genérico 2",DataNascimento=DateTime.Parse("2017-12-10"),ProfessorID = 3}
            };

            alunos.ForEach(s => context.Alunos.Add(s));
            context.SaveChanges();


        }
    }
}