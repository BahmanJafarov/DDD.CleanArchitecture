using MediatR;
using System;
using System.Text;

namespace AppDomain.Common.DomainEvents
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime DateOccured { get; protected set; } = DateTime.UtcNow;
    }
}
