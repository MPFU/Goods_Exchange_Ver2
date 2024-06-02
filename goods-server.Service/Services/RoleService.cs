using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateNewRole(RoleDTO roleDTO)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDTO);
                await _unitOfWork.RoleRepo.AddAsync(role);
                var result = await _unitOfWork.SaveAsync() > 0;
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRole(int RoleId)
        {
            try
            {
                var check = await _unitOfWork.RoleRepo.GetRoleById(RoleId);
                if(check == null)
                {
                    return false;
                }
                _unitOfWork.RoleRepo.Delete(check);
                var result = await _unitOfWork.SaveAsync() > 0;
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<GetRoleDTO> GetRoleById(int roleId)
        {
            return _mapper.Map<GetRoleDTO>(await _unitOfWork.RoleRepo.GetRoleById(roleId));
        }

        public async Task<IEnumerable<GetRoleDTO>> GetRoles()
        {
            return _mapper.Map<IEnumerable<GetRoleDTO>>(await _unitOfWork.RoleRepo.GetAllAsync());
        }

        public async Task<bool> UpdateRole(int RoleId, RoleDTO roleDTO)
        {
            try
            {
                var check = await _unitOfWork.RoleRepo.GetRoleById(RoleId);
                if (check == null)
                {
                    return false;
                }
                check.Name = roleDTO.Name;
                _unitOfWork.RoleRepo.Update(check);
                var result = await _unitOfWork.SaveAsync() > 0;
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
