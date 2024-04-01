using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crashstack.Data.Entities
{
    public class Trace
    {
        public Guid Id { get; set; }
        public string? TraceId { get; set; }
        public string? Release { get; set; }
        public string? Environment { get; set; }
        public string? Message { get; set; }
    }
}
