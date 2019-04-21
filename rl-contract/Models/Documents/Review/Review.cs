using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rl_contract.Models.Review
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        /*
         * Review Overview
         */
        public bool Payed { get; set; }
        public string Exercises { get; set; }
        public string Reasons { get; set; }
        /*
         * Review Details
         */
        public string ObservationsParents { get; set; }
        public string ObservationsChild { get; set; }
        public string ExerciseAccomplishment { get; set; }
        public List<ProblemHierarchie> ProblemHierarchies { get; set; }
        public List<ReviewChapter> Chapters { get; set; }

        public int? PatientId { get; set; }

    }

    public class ProblemHierarchie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string InitialValue { get; set; }
        public string ChangedValue { get; set; }

        public int? ReviewId { get; set; }

    }

    public class ReviewChapter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }

        public List<ReviewQuestion> Questions { get; set; }

        public int? ReviewId { get; set; }
    }

    public class ReviewQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public int? ReviewChapterId { get; set; }
    }
}
