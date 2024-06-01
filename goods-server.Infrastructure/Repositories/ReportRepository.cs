using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class ReportRepository : GenericRepository<Report>,IReportRepo
    {
        public ReportRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
