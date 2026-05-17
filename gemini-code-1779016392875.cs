using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KibabiiRevisionGroup.Models
{
    public class AssessmentResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RegNumber { get; set; } = string.Empty;

        [Required]
        public string SubjectOrModule { get; set; } = string.Empty;

        [Required]
        public int Score { get; set; }

        [Required]
        public int TotalQuestions { get; set; }

        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}