using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSOLUTE_CINEMA.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Текст комментария обязателен")]
        [StringLength(500, ErrorMessage = "Максимальная длина - 500 символов")]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}