using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DemoProject.Models
{
    public class Configuration
    {
        /// <summary>
        /// Building information id.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        /// <summary>
        /// Building type.
        /// </summary>
        public string BuildingType { get; set; } = "";

        /// <summary>
        /// Building cost.
        /// </summary>
        public int BuildingCost { get; set; } = 0;

        /// <summary>
        /// Building construction time.
        /// </summary>
        public int ConstructionTime { get; set; } = 0;
    }
}
