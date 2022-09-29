using AutoMapper;
using Queries.Notes;

namespace Note.API.View.Notes
{
    public class ViewNoteModelDto
    {
        public ulong Id { get; set; }
        public string Titel { get; set; }
        public string Detailse { get; set; }
        public bool IsCmpleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditTame { get; set; }
    }
}
