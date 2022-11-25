namespace MinimalAPIMongoDB.Models;

public record Developer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    [BsonElement("developer_name")]
    public string DeveloperName { get; set; }
    
    [BsonElement("aggregation")]
    public Aggregation Aggregation { get; set; }
    
    [BsonElement("technologies")]
    public int[] Technologies { get; set; }
}

public record Aggregation
{
    [BsonElement("aggregation_id")]
    public int AggregationId { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
}