using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TesteBRConselhos.Models;

namespace TesteBRConselhos.DAL
{
    public class SistemaContext : DbContext
    {

        public SistemaContext() : base("SistemaContext")
        {
        }

        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}