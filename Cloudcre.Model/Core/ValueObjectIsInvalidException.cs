using System;

namespace Cloudcre.Model.Core
{
    public class ValueObjectIsInvalidException : Exception
    {
        public ValueObjectIsInvalidException(string message)
            : base(message)
        {
        }
    }
}