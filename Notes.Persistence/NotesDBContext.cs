using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfigurations;
using Notes.Application.Intarfaces;

namespace Notes.Persistence
{
    public class NotesDBContext : DbContext, INotesDBContext
    {
        public DbSet<Note> Notes { get; set; }
        public NotesDBContext(DbContextOptions<NotesDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NotesConfigurations());
            base.OnModelCreating(builder);
        }
    }
}
