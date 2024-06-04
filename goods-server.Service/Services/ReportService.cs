using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateReportAsync(ReportDTO reportDto)
        {
            var report = _mapper.Map<Report>(reportDto);
            report.ReportId = Guid.NewGuid(); // Tạo một Guid mới
            report.PostDate = DateTime.UtcNow;
            await _unitOfWork.ReportRepo.AddAsync(report);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }



        public async Task<IEnumerable<GetReportDTO>> GetReportsByAccountIdAsync(Guid accountId)
        {
            var reports = await _unitOfWork.ReportRepo.GetReportsByAccountIdAsync(accountId);
            return _mapper.Map<IEnumerable<GetReportDTO>>(reports);
        }

        public async Task<bool> UpdateReportAsync(Guid reportId, UpdateReportDTO reportDto)
        {
            var existingReport = await _unitOfWork.ReportRepo.GetReportByIdAsync(reportId);
            if (existingReport == null)
            {
                return false;
            }

            existingReport.Descript = reportDto.Descript;
            existingReport.Status = reportDto.Status; // Cập nhật Status
            _unitOfWork.ReportRepo.Update(existingReport);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }


        public async Task<bool> DeleteReportAsync(Guid reportId)
        {
            return await _unitOfWork.ReportRepo.DeleteReportAsync(reportId);
        }

        public async Task<IEnumerable<GetReportDTO>> GetAllReportsAsync()
        {
            var reports = await _unitOfWork.ReportRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<GetReportDTO>>(reports);
        }

        public async Task<GetReportDTO> GetReportByIdAsync(Guid reportId)
        {
            var report = await _unitOfWork.ReportRepo.GetReportByIdAsync(reportId);
            return _mapper.Map<GetReportDTO>(report);
        }

    }


}
