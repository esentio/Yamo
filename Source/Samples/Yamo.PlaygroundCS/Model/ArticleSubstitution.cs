using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.PlaygroundCS.Model
{
    class ArticleSubstitution
    {
        public int OriginalArticleId { get; set; }
        public int SubstitutionArticleId { get; set; }

        public Article Original { get; set; }
        public Article Substitution { get; set; }
    }
}
