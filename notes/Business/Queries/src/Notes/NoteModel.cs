using AutoMapper;
using Notes.Domain.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Notes
{
    public class NoteModel
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Titel { get; set; }
        public bool IsCmpleted { get; set; }
        public string Detailse { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditTame { get; set; }
    }
}
