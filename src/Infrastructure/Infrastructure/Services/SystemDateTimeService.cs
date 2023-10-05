using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class SystemDateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}