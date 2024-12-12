using LiteDB;

public class LiteDbService
{
    private readonly LiteDatabase _database;

    public LiteDbService()
    {
        _database = new LiteDatabase("Filename=todo-nosql.db; Connection=shared;");
    }

    public LiteDatabase Database => _database;

    public ILiteCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
