using System.ComponentModel.DataAnnotations;

namespace Ghost.API.Models
{
    public class PlayDto
    {
        [Required(ErrorMessage = "Text field should be specified.")]
        [MaxLength(30, ErrorMessage = "Maximum allowed text for word field is 30.")]
        [MinLength(1, ErrorMessage = "Minimum allowed length for text field is 1.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Text field should only include letters.")]
        public string Text { get; set; }
    }
}
