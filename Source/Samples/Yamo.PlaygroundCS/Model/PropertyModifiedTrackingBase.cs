using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.PlaygroundCS.Model
{
    public abstract class PropertyModifiedTrackingBase : IHasPropertyModifiedTracking
    {

        //  NOTE: this is very naive and memory consuming implementation of IHasPropertyModifiedTracking
        private HashSet<string> m_Modified = new HashSet<string>();

        protected void MarkPropertyAsModified(string propertyName)
        {
            m_Modified.Add(propertyName);
        }

        public bool IsAnyPropertyModified()
        {
            return m_Modified.Any();
        }

        public bool IsPropertyModified(string propertyName)
        {
            return m_Modified.Contains(propertyName);
        }

        public void ResetPropertyModifiedTracking()
        {
            m_Modified.Clear();
        }
    }
}
