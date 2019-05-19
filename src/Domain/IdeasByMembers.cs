using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// Ideas - grouped by members
    /// </summary>
    public class IdeasByMembers
    {
        /// <summary>
        /// Team member
        /// </summary>
        public string Member { get; set; }

        /// <summary>
        /// Team names
        /// </summary>
        public IEnumerable<string> TeamNames { get; set; }
    }
}