using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Core.InterfacesRepo
{
    public interface IReportRepo : IGenericRepo<Report>
    {
        Task<IEnumerable<Report>> GetReportsByAccountIdAsync(Guid accountId);
        Task<bool> UpdateReportAsync(Guid reportId, Report report);
        Task<bool> DeleteReportAsync(Guid reportId);
        Task<Report> GetReportByIdAsync(Guid reportId);

    }

}
