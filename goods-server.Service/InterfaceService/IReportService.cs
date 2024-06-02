using goods_server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IReportService
    {
        Task<bool> CreateReportAsync(ReportDTO report);
        Task<IEnumerable<ReportDTO>> GetReportsByAccountIdAsync(Guid accountId);
        Task<IEnumerable<ReportDTO>> GetAllReportsAsync();
        Task<bool> UpdateReportAsync(Guid reportId, ReportDTO report);
        Task<bool> DeleteReportAsync(Guid reportId);
        Task<ReportDTO> GetReportByIdAsync(Guid reportId);

    }

}
