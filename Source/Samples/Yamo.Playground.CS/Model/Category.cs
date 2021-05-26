using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.Playground.CS.Model
{
    class Category
    {
        public int Id { get; set; }
        public Label Label { get; set; }
        
        public int ArticleCount { get; set; }
    }
}
