using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.Playground.CS.Model
{
    class ArticlePart
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public decimal Price { get; set; }

        public Label Label { get; set; }
    }
}
