using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rl_contract.Models
{
    public class TestungQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        /*public TestungBaseData Data { get; set; }*/
        public string Type { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public int? TestungChapterId { get; set; }

    }
    
}
