using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistence.MySQL
{
    public class DbInitializer
    {
        public static void Initializer(AppDbContext context) 
        {
            context.Database.EnsureCreated();
        }

        public static void AddMigration(AppDbContext context) 
        {
            context.Database.Migrate();
        }
    }
}
