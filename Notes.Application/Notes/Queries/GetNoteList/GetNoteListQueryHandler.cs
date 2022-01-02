using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notes.Application.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDBContext _dbContext;
        private readonly IMapper _mapper;
        public GetNoteListQueryHandler(INotesDBContext dBContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dBContext, mapper);

        public async Task<NoteListVm> Handle (GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _dbContext.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new NoteListVm { Notes = notesQuery };
        }
    }
}
