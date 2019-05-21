using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteBRConselhos.Models
{
    public class Professor
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Aluno> Alunos { get; set; }
    }
}