using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo_List.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Required]
        public string StartDay { get; set; }
        [Required]
        public string EndDay { get; set; }
        public bool IsComplete { get; set; }
        public bool IsDisplay { get; set; }
        public string UserId { get; set; }
    }
}
