using System;
using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// Group data suitable to be displayed in a chart
    /// </summary>
    public class ChartViewModel
    {
        /// <summary>
        /// Ideas by members
        /// </summary>
        public IEnumerable<IdeasByMembers> ByMembers { get; set; }

        /// <summary>
        /// Ideas by dates
        /// </summary>
        public IEnumerable<IdeasByDate> ByDate { get; set; }
    }
}