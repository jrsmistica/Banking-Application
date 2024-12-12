using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_4
{
    internal class LoginEventArgs
    {
        public class LoginEventArgs : EventArgs
        {
            public string PersonName { get; }
            public bool Success { get; }

            public LoginEventArgs(string personName, bool success)
            {
                PersonName = personName;
                Success = success;
            }
        }

    }
}
