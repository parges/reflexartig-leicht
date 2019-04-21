using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using rl_contract.Models.Review;

namespace rl_contract.Models
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Tele { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime? AnamneseDate { get; set; }
        public bool? AnamnesePayed { get; set; }
        public DateTime? DiagnostikDate { get; set; }
        public bool? DiagnostikPayed { get; set; }
        public DateTime? ElternDate { get; set; }
        public bool? ElternPayed { get; set; }
        public string ProblemHierarchy { get; set; }

        public List<Review.Review> Reviews { get; set; }
        public Testung Testung { get; set; }
        public Anamnese Anamnese { get; set; }

    }
}
