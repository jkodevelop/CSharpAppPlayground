namespace CSharpAppPlayground.DBClasses.Data
{
    public class SqlDBObject
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return $"SqlDBObject: Id={Id}, Name={Name}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
