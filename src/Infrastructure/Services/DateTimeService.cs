using RetailInMotion.Application.Common.Interfaces;

namespace RetailInMotion.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}