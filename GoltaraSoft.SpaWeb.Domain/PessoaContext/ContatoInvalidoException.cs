using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.PessoaContext.Pessoa
{
    [Serializable()]
    public class ContatoInvalidoException : DomainException
    {
        public ContatoInvalidoException() : base()
        { }
        public ContatoInvalidoException(string message) : base(message)
        { }
        public ContatoInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
