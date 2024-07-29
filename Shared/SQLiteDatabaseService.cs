using SQLite;

public class SQLiteDatabaseService
{
    private SQLiteAsyncConnection _database;

    public SQLiteDatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<NotificationData>().Wait();
    }

    public async Task<List<NotificationData>> GetNotificationsAsync()
    {
        return await _database.Table<NotificationData>().ToListAsync();
    }

    public async Task<int> SaveNotificationAsync(NotificationData notification,bool Updating)
    {
        if (Updating)
        {
            return await _database.UpdateAsync(notification);
        }
        else
        {
            return await _database.InsertAsync(notification);
        }
    }

    public async Task<int> DeleteNotificationAsync(NotificationData notification)
    {
        return await _database.DeleteAsync(notification);
    }
}

public class NotificationData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsPaid { get; set; }
}