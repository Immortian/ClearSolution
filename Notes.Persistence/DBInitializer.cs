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
