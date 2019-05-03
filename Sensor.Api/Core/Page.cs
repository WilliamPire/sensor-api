using System.Collections.Generic;

namespace Sensor.Api.Core
{
    public class Page<T> where T : class
    {
        public IList<T> Items { get; set; }
        public long TotalRegistros { get; set; }
    }
}
