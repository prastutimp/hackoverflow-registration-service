using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain
{
    /// <summary>
    /// The idea
    /// </summary>
    public class Idea
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Team name
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Brief description on the idea
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Team member 1
        /// </summary>
        public string Member1 { get; set; }

        /// <summary>
        /// Team member 2
        /// </summary>
        public string Member2 { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Shortlisted idea?
        /// </summary>
        public bool Shortlisted { get; set; }
    }
}