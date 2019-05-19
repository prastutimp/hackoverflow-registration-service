using System;
using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// Ideas grouped by dates
    /// </summary>
    public class IdeasByDate
    {
        /// <summary>
        /// Created on
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Team names
        /// </summary>
        public IEnumerable<string> TeamNames { get; set; }
    }
}