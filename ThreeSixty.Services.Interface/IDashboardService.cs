using ThreeSixty.Dto;

namespace ThreeSixty.Services.Interface
{
    public interface IDashboardService
    {
        public Task<DashboardDto> GetDashboard();



    }
}