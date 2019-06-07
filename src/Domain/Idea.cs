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
        ///Name of the idea
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Brief description on the idea
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Buisiness benifit of the idea
        /// </summary>
        public string Benefits { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// created by
        /// </summary>
        public User CreatedBy { get; set; }

        /// <summary>
        /// Shortlisted idea?
        /// </summary>
        public bool Shortlisted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InstanceName { get; set; }
    }
}