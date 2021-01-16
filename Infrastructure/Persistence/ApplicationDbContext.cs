using AppDomain.Common.DomainEvents;
using AppDomain.Common.Entities;
using AppDomain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService, IMediator mediator)
            : base(options)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAudited>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationAuditedProperties(entry);
                        break;
                    case EntityState.Modified:
                        SetModificationAuditedProperties(entry);
                        break;
                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                        SetDeletionAuditedProperties(entry);
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (_mediator == null)
            {
                return result;
            }

            var entitiesWithEvents = ChangeTracker.Entries<HaveDomainEvents>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();

                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }

            return result;
        }


        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public DbSet<Person> Persons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
