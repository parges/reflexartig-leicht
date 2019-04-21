using System;
using System.Collections.Generic;
using System.Text;

namespace kubaapi.utils
{
    /// <summary>
    /// QueryResponse
    /// </summary>
    public class QueryResponse<T>
    {
        /// <summary>
        /// TotalRecords
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
