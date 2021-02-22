using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    [Serializable()]
    public class OrigemInvalidoException : DomainException
    {
        public OrigemInvalidoException() : base()
        { }
        public OrigemInvalidoException(string message) : base(message)
        { }
        public OrigemInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
