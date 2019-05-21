using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteBRConselhos.Models
{
    public class Aluno
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        [NotMapped]
        public int idade { get; set; }

        public int ProfessorID { get; set; }

        public virtual Professor Professor { get; set; }
    }
}


