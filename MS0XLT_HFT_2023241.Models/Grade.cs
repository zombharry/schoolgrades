using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MS0XLT_HFT_2023241.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }

        
        public int SubjectId { get; set; }

        public int StudentId { get; set; }

        [Range(1,5)]
        public int GradeValue { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public virtual Subject Subject { get; set; }
    }
}
