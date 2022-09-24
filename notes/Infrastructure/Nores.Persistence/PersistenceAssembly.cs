using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistence.MySQL
{
    public static class PersistenceAssembly
    {
        public static Assembly value = typeof(PersistenceAssembly).Assembly;
    }
}
