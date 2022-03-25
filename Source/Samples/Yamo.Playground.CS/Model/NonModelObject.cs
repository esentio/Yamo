using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.Playground.CS.Model
{
    class NonModelObject
    {
        public int Id { get; set; }

        public string Description { get; set; }
        
        public object Item { get; set; }

        public NonModelObject(int id)
        {
            this.Id = id;
        }

        public NonModelObject(int id, object item) : this(id)
        {
            this.Item = item;
        }
    }
}
