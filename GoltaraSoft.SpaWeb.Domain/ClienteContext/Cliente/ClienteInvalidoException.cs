using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    [Serializable()]
    public class ClienteInvalidoException : DomainException
    {
        public ClienteInvalidoException() : base()
        { }
        public ClienteInvalidoException(string message) : base(message)
        { }
        public ClienteInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
