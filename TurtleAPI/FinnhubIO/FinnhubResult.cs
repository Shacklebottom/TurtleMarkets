using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleAPI.FinnhubIO
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class FinnhubResult
    {
        public float c { get; set; }
        public float d { get; set; }
        public float dp { get; set; }
        public float h { get; set; }
        public float l { get; set; }
        public float o { get; set; }
        public float pc { get; set; }
        public float t { get; set; }
    }
}
