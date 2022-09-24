using Microsoft.EntityFrameworkCore;
using Notes.Domain.Notes;
using Notes.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Domain.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<User> Users { get; set; }
       Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
