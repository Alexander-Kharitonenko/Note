using AutoMapper;
using Commands.Notes.Update;

namespace Note.API.View.Notes
{
    public class UpdateNoteDto
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public bool isCompleted { get; set; }
    }
}
