using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightTickets.Models.Entities.Enums;

public enum EStatus
{
    [BsonRepresentation(BsonType.String)]
    Pending,
    [BsonRepresentation(BsonType.String)]
    Approved,
    [BsonRepresentation(BsonType.String)]
    Denied
}