using AutoMapper;
using Queries.Notes;

namespace Note.API.View.Notes
{
    public class ViewNoteModelDto
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditTame { get; set; }
    }
}
