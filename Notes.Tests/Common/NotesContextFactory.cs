using Microsoft.EntityFrameworkCore;
using Notes.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDBContext Create()
        {
            var option = new DbContextOptionsBuilder<NotesDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDBContext(option);
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                new Domain.Note
                {
                    CreationDate = DateTime.Today,
                    Details = "someDetails",
                    EditDate = null,
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    UserId = UserAId,
                    Title = "Note name"
                },
                new Domain.Note
                {
                    CreationDate = DateTime.Today,
                    Details = "someDetails 2",
                    EditDate = null,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    UserId = UserBId,
                    Title = "Note name 2"
                },
                new Domain.Note
                {
                    CreationDate = DateTime.Today,
                    Details = "someDetails 3",
                    EditDate = null,
                    Id = NoteIdForDelete,
                    UserId = UserAId,
                    Title = "Note name 3"
                },
                new Domain.Note
                {
                    CreationDate = DateTime.Today,
                    Details = "someDetails 4",
                    EditDate = null,
                    Id = NoteIdForUpdate,
                    UserId = UserBId,
                    Title = "Note name 4"
                });
            context.SaveChanges();
            return context;
        }
        public static void Destroy(NotesDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
