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
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            var genres = await _unitOfWork.GenreRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDTO>>(genres);
        }

        public async Task<GenreDTO?> GetGenreByIdAsync(Guid genreId)
        {
            var genre = await _unitOfWork.GenreRepo.GetByIdAsync(genreId);
            return _mapper.Map<GenreDTO>(genre);
        }

        public async Task<GenreDTO?> GetGenreByNameAsync(string name)
        {
            var genre = await _unitOfWork.GenreRepo.GetByNameAsync(name);
            return _mapper.Map<GenreDTO>(genre);
        }

        public async Task<bool> CreateGenreAsync(CreateGenreDTO genre)
        {
            var newGenre = _mapper.Map<Genre>(genre);
            newGenre.GenreId = Guid.NewGuid();
            await _unitOfWork.GenreRepo.AddAsync(newGenre);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateGenreAsync(Guid genreId, UpdateGenreDTO genre)
        {
            var existingGenre = await _unitOfWork.GenreRepo.GetByIdAsync(genreId);
            if (existingGenre == null)
            {
                return false;
            }

            _mapper.Map(genre, existingGenre);
            _unitOfWork.GenreRepo.Update(existingGenre);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteGenreAsync(Guid genreId)
        {
            var genre = await _unitOfWork.GenreRepo.GetByIdAsync(genreId);
            if (genre == null)
            {
                return false;
            }

            _unitOfWork.GenreRepo.Delete(genre);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
