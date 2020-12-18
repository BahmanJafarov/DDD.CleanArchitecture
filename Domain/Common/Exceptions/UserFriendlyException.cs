using System;
using System.Collections.Generic;
using System.Text;

namespace AppDomain.Common.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string errorMessage)
            : this(errorMessage, null)
        {

        }

        public UserFriendlyException(string errorMessage, Exception exc)
            : base($"The following error has occured \"{errorMessage}\"", exc)
        {

        }
    }
}
