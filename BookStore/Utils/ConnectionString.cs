namespace BookStore.Utils
{
    public class ConnectionString
    {
        public static string GetString()
        {
            return "Host=localhost;Port=5432;Database=book_store;Username=postgres;Password=123";
        }
    }
}
