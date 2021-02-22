using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.PessoaContext.Pessoa
{
    public class PessoaInvalidaException : DomainException
    {
        public PessoaInvalidaException() : base()
        { }
        public PessoaInvalidaException(string message) : base(message)
        { }
        public PessoaInvalidaException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
