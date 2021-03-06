using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteEdit
    {
        [Required]
        public int NoteId { get; set; }

        [MaxLength(100, ErrorMessage = "There are too many characters in this field")]
        public string Title { get; set; }

        [MaxLength(8000, ErrorMessage = "There are too many characters in this field")]
        public string Content { get; set; }
        public bool IsStarred { get; set; }
    }
}
