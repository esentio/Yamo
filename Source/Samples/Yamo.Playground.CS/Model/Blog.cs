using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.Playground.CS.Model
{
    class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime? Modified { get; set; }

        public int? ModifiedUserId { get; set; }

        public DateTime? Deleted { get; set; }

        public int? DeletedUserId { get; set; }
    }
}
