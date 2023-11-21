using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [StringLength(25)]
        public string StudentName { get; set; }

        public int Semester { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
