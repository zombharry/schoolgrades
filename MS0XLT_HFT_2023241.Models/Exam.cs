using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public int StudentId { get; set; }

        public int Grade { get; set; }

       
    }
}
