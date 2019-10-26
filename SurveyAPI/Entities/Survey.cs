using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyAPI.Entities
{
    public class Survey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Survey_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Survey_Name { get; set; }

        public string Survey_Options { get; set; }

        public string Survey_Finished_Users { get; set; }

    }
}
