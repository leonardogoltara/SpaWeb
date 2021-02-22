using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.ServicoContext
{
    [Serializable()]
    public class ServicoInvalidoException : DomainException
    {
        public ServicoInvalidoException() : base()
        { }
        public ServicoInvalidoException(string message) : base(message)
        { }
        public ServicoInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
