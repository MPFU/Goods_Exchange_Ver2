using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepo
    {
        public ReportRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Report>> GetReportsByAccountIdAsync(Guid accountId)
        {
            return await _dbContext.Reports.Where(x => x.AccountId == accountId).ToListAsync();
        }

        public async Task<bool> UpdateReportAsync(Guid reportId, Report report)
        {
            var existingReport = await _dbContext.Reports.FindAsync(reportId);
            if (existingReport == null)
            {
                return false;
            }

            existingReport.Descript = report.Descript;
            existingReport.Status = report.Status;
            _dbContext.Reports.Update(existingReport);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReportAsync(Guid reportId)
        {
            var report = await _dbContext.Reports.FindAsync(reportId);
            if (report == null)
            {
                return false;
            }

            _dbContext.Reports.Remove(report);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}
