using MinimalAPIMongoDB.Data;

namespace MinimalAPIMongoDB.Services;

public class DeveloperService
{
    private readonly IMongoCollection<Developer> _developers;

    public DeveloperService(IOptions<DeveloperDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _developers = mongoClient
            .GetDatabase(options.Value.DatabaseName)            ;
            .GetCollection<Developer>(options.Value.CollectionName);
    }
    
    public async Task<List<Developer>> GetAll () =>
        await _developers.Find(_ => true).ToListAsync();
    
    public async Task<Developer> Get(string id) =>
        await._developers.Find(d => d._id == id).FirstOrDefaultAsync();
    
    public async Task Create(Developer developer) =>
        await _developers.InsertOneAsync(developer);
    
    public async Task Update(string id, Developer developer) =>
        await _developers.ReplaceOneAsync(d => d._id == id, developer);
    
    public async Task Delete(string id) =>
        await _developers.DeleteOneAsync(d => d._id == id);
}