using Microsoft.EntityFrameworkCore;
using Notes.Domain.Interfaces;
using Notes.Domain.Notes;
using Notes.Domain.Users;
using Notes.Persistence.MySQL.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Persistence.MySQL
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) 
        {}

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new NotesConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await SaveChangesAsync(true, cancellationToken);
        }
    }
}
