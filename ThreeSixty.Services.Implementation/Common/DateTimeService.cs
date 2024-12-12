using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Services.Implementation.Common
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
