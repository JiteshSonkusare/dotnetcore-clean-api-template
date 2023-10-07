using Domain.Entities;
using Domain.Contracts;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class ApplicationDBContext : DbContext
{
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IDateTimeService dateTimeService) : base(options)
    {
        _dateTimeService = dateTimeService;
    }

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
        .SelectMany(t => t.GetProperties())
        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = _dateTimeService.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = _dateTimeService.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}