using Application.Common.Interfaces;

namespace Infrastructure.Services.Common;

public class SystemDateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}