using AutoMapper;
using Commands.Notes.Create;

namespace Note.API.View.Notes
{
    public class CreateNoteDto
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public ulong UserId { get; set; }
    }
}
