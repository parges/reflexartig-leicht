using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rl_contract.Models
{
    public class AnamneseQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string MetaInfo { get; set; }
        public string TextPrefix { get; set; }
        public string TextValue { get; set; }

        public int? AnamneseChapterId { get; set; }

    }
    
}
