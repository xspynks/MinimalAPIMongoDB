namespace StudentManager.Services;

public class StudentService
{
    private readonly IMongoCollection<Student> _students;

    public StudentService(IOptions<StudentDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _students = mongoClient
            .GetDatabase(options.Value.DatabaseName)
            .GetCollection<Student>(options.Value.CollectionName);
    }

    public async Task<List<Student>> GetAll() =>
        await _students.Find(_ => true).ToListAsync();

    public async Task<Student> Get(string id) =>
        await _students.Find(s => s._id == id).FirstOrDefaultAsync();

    public async Task Create(Student student) =>
        await _students.InsertOneAsync(student);

    public async Task Update(string id, Student student) =>
        await _students.ReplaceOneAsync(s => s._id == id, student);

    public async Task Delete(string id) =>
        await _students.DeleteOneAsync(s => s._id == id);
}