using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.EmpresaContext
{
    [Serializable()]
    public class EmpresaInvalidoException : DomainException
    {
        public EmpresaInvalidoException() : base()
        { }
        public EmpresaInvalidoException(string message) : base(message)
        { }
        public EmpresaInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
