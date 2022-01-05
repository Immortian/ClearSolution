using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.Intarfaces;
using Notes.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public NotesDBContext Context;
        public IMapper Mapper;
        public QueryTestFixture()
        {
            Context = NotesContextFactory.Create();
            var configureationBuilder = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(INotesDBContext).Assembly));
            });
            Mapper = configureationBuilder.CreateMapper();
        }

        public void Dispose()
        {
            NotesContextFactory.Destroy(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture>
        {

        }
    }
}
