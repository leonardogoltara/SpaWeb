using GoltaraSolutions.Common.Domain.Exceptions;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento
{
    [Serializable()]
    public class AtendimentoInvalidoException : DomainException
    {
        public AtendimentoInvalidoException() : base()
        { }
        public AtendimentoInvalidoException(string message) : base(message)
        { }
        public AtendimentoInvalidoException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
