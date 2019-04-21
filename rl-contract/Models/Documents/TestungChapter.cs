using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rl_contract.Models
{
    public class TestungChapter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }

        public int? TestungId { get; set; }
        public List<TestungQuestion> Questions { get; set; }

    }
}
