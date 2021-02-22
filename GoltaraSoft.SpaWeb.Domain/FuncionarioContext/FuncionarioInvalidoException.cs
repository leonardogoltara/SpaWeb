using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.FuncionarioContext
{
    [Serializable()]
    public class FuncionarioInvalidoException : DomainException
    {
        public FuncionarioInvalidoException() : base()
        { }
        public FuncionarioInvalidoException(string message) : base(message)
        { }
        public FuncionarioInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
