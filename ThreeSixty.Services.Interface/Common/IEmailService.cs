using ThreeSixty.Common;

namespace ThreeSixty.Services.Interface.Common
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
