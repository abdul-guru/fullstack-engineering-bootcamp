namespace TaskFlow.Api.Data;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; } = "";
    public string DatabaseName { get; set; } = "";
}
