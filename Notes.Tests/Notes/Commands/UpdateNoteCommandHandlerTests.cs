using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exeptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            //Arrange (подготовка данных)
            var handler = new UpdateNoteCommandHandler(Context);
            var updatedTitle = "Note name 7";

            //Act (выполнение логики)
            await handler.Handle(
                new UpdateNoteCommand
                {
                    Title = updatedTitle,
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None);

            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Title == updatedTitle &&
                note.Id == NotesContextFactory.NoteIdForUpdate));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            //Arrange (подготовка данных)
            var handler = new UpdateNoteCommandHandler(Context);

            //Act (выполнение логики)
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new UpdateNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            //Arrange (подготовка данных)
            var handler = new UpdateNoteCommandHandler(Context);

            //Act (выполнение логики)
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None));
        }
    }
}
