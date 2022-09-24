using System;

namespace Notes.Domain.Notes
{
    public class Note
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Titel { get; set; }
        public string Detailse { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditTame { get; set; }  
    }
}
