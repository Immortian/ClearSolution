using Notes.Tests.Common;
using Notes.Application.Notes.Commands.CreateNote;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Notes.Tests.Notes.Commands
{
    public class CreaNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange (подготовка данных)
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "Note name 5";
            var noteDetails = "note details 5";

            //Act (выполнение логики)
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetails,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);

            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId &&
                note.Title == noteName &&
                note.Details == noteDetails));
        }
    }
}
