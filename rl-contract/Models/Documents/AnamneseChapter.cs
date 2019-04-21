using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rl_contract.Models
{
    public class AnamneseChapter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Name { get; set; }

        public int? AnamneseId { get; set; }
        public List<AnamneseQuestion> Questions { get; set; }

    }
}
