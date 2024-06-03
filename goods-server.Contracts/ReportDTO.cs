using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class ReportDTO
    {
        public Guid? AccountId { get; set; }
        public Guid? ProductId { get; set; }
        public string? Descript { get; set; }
        public string? Status { get; set; }
    }

    public class GetReportDTO
    {
        public Guid ReportId { get; set; }

        public Guid? AccountId { get; set; }

        public Guid? ProductId { get; set; }

        public DateTime? PostDate { get; set; }

        public string? Descript { get; set; }

        public string? Status { get; set; }
    }

    public class UpdateReportDTO 
    {
        public string? Descript { get; set; }

    }

}
