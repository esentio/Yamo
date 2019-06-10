using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.PlaygroundCS.Model
{
    public abstract class PropertyModifiedTrackingBase : IHasDbPropertyModifiedTracking
    {

        //  NOTE: this is very naive and memory consuming implementation of IHasDbPropertyModifiedTracking
        private HashSet<string> m_Modified = new HashSet<string>();

        protected void MarkPropertyAsModified(string propertyName)
        {
            m_Modified.Add(propertyName);
        }

        public bool IsAnyDbPropertyModified()
        {
            return m_Modified.Any();
        }

        public bool IsDbPropertyModified(string propertyName)
        {
            return m_Modified.Contains(propertyName);
        }

        public void ResetDbPropertyModifiedTracking()
        {
            m_Modified.Clear();
        }
    }
}
