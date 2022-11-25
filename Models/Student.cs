namespace MinimalAPIMongoDB.Models;

public record Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    [BsonElement("student_name")]
    public string StudentName { get; set; }
    
    [BsonElement("school")]
    public School School { get; set; }
    
    [BsonElement("marks")]
    public int[] Marks { get; set; }
}

public record School
{
    [BsonElement("school_id")]
    public int SchoolID { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("address")]
    public string Address { get; set; }
    
    [BsonElement("city")]
    public string City { get; set; }
    
    [BsonElement("state")]
    public string State { get; set; }
    
    [BsonElement("zipcode")]
    public string Zipcode { get; set; }
}