using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.ProdutoContext
{
    [Serializable()]
    public class ProdutoInvalidoException : DomainException
    {
        public ProdutoInvalidoException() : base()
        { }
        public ProdutoInvalidoException(string message) : base(message)
        { }
        public ProdutoInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
