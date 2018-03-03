using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.PlaygroundCS.Model
{
    class Article
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public Label Label { get; set; }
        public List<ArticlePart> Parts { get; set; }
        public List<Category> Categories { get; set; }
    }
}
