using MongoDB.Driver;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Data;

public sealed class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskItem> _collection;

    public TaskRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TaskItem>("tasks");
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
    {
        return await _collection.Find(Builders<TaskItem>.Filter.Empty).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(string id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(x => x.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(TaskItem item)
    {
        await _collection.InsertOneAsync(item);
    }

    public async Task<bool> UpdateAsync(string id, TaskItem item)
    {
        var filter = Builders<TaskItem>.Filter.Eq(x => x.Id, id);
        var update = Builders<TaskItem>.Update
            .Set(x => x.Title, item.Title)
            .Set(x => x.Description, item.Description)
            .Set(x => x.Status, item.Status);

        var result = await _collection.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(x => x.Id, id);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
