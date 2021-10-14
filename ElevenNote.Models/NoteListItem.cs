using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteListItem
    {
        [Required]
        public int NoteId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [UIHint("Starred")]
        [Display(Name = "Important")]
        public bool isStarred { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
