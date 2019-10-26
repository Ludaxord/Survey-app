using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAPI.Entities
{
    [Table("QuestionsTypes")]
    public class QuestionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Question_Type_Id { get; set; }
        [Required]
        public string Question_Type_Name { get; set; }

    }
}
