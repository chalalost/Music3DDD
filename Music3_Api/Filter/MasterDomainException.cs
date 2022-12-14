using System;

namespace Music3_Api.Filter
{
    public partial class MasterDomainException : Exception
    {
        public MasterDomainException()
        { }

        public MasterDomainException(string message)
            : base(message)
        { }

        public MasterDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
