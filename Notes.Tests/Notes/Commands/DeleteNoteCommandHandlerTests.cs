using Notes.Tests.Common;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Common.Exeptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            //Arrange (подготовка данных)
            var handler = new DeleteNoteCommandHandler(Context);


            //Act (выполнение логики)
            await handler.Handle(new DeleteNoteCommand
            {
                Id = NotesContextFactory.NoteIdForDelete,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None);

            Assert.Null(
                Context.Notes.SingleOrDefault(note =>
                note.Id == NotesContextFactory.NoteIdForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            //Arrange (подготовка данных)
            var handler = new DeleteNoteCommandHandler(Context);

            //Act (выполнение логики)
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            //Arrange (подготовка данных)
            var deletehandler = new DeleteNoteCommandHandler(Context);
            var createhandler = new CreateNoteCommandHandler(Context);

            var noteId = await createhandler.Handle(
                new CreateNoteCommand
                {
                    Title = "Note name 6",
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);

            //Act (выполнение логики)
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deletehandler.Handle(new DeleteNoteCommand
                {
                    Id = noteId,
                    UserId = NotesContextFactory.UserBId
                }, CancellationToken.None));
        }
    }
}
