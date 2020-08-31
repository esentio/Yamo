using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamo.Playground.CS.Model
{
    class User2 : PropertyModifiedTrackingBase
    {
        private int m_Id;
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; MarkPropertyAsModified(nameof(Id)); }
        }

        private string m_Login;
        public string Login
        {
            get { return m_Login; }
            set { m_Login = value; MarkPropertyAsModified(nameof(Login)); }
        }

        private string m_FirstName;
        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; MarkPropertyAsModified(nameof(FirstName)); }
        }

        private string m_LastName;
        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; MarkPropertyAsModified(nameof(LastName)); }
        }

        private string m_Email;
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; MarkPropertyAsModified(nameof(Email)); }
        }
    }
}
