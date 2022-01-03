namespace Notes.Identity.Data
{
    public class DBInitializer
    {
        public static void Initialize(AuthDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
