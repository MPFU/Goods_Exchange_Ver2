using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetAllCitiesAsync()
        {
            var cities = await _unitOfWork.CityRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CityDTO>>(cities);
        }

        public async Task<CityDTO?> GetCityByIdAsync(Guid cityId)
        {
            var city = await _unitOfWork.CityRepo.GetByIdAsync(cityId);
            return _mapper.Map<CityDTO>(city);
        }

        public async Task<CityDTO?> GetCityByNameAsync(string name)
        {
            var city = await _unitOfWork.CityRepo.GetByNameAsync(name);
            return _mapper.Map<CityDTO>(city);
        }

        public async Task<bool> CreateCityAsync(CreateCityDTO city)
        {
            var newCity = _mapper.Map<City>(city);
            newCity.CityId = Guid.NewGuid();
            await _unitOfWork.CityRepo.AddAsync(newCity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateCityAsync(Guid cityId, UpdateCityDTO city)
        {
            var existingCity = await _unitOfWork.CityRepo.GetByIdAsync(cityId);
            if (existingCity == null)
            {
                return false;
            }

            _mapper.Map(city, existingCity);
            _unitOfWork.CityRepo.Update(existingCity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteCityAsync(Guid cityId)
        {
            var city = await _unitOfWork.CityRepo.GetByIdAsync(cityId);
            if (city == null)
            {
                return false;
            }

            _unitOfWork.CityRepo.Delete(city);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
