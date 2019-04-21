using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rl_contract.Models
{
    public class Testung
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<TestungChapter> Chapters { get; set; }

        public int? PatientId { get; set; }
        /*public Patient Patient { get; set; }*/

    }
}
