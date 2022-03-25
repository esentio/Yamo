using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.Playground.CS.Model
{
    struct NonModelStruct
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public object Item { get; set; }

        public NonModelStruct(int id) : this()
        {
            this.Id = id;
        }
    }
}
