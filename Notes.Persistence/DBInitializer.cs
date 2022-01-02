using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistence
{
    public class DBInitializer
    {
        public static void Initialize(NotesDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
