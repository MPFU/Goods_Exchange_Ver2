using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateProduct(CreateProductDTO createProductDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(createProductDTO);
                product.CreatedDate = DateTime.UtcNow;
                product.ProductId = Guid.NewGuid();
                await _unitOfWork.ProductRepo.AddAsync(product);
                var result = await _unitOfWork.SaveAsync() > 0;
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            };
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
                if(product != null)
                {
                    _unitOfWork.ProductRepo.Delete(product);
                    var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<GetProductDTO> GetProduct(Guid id)
        {
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            return _mapper.Map<GetProductDTO>(product);
        }

        public async Task<bool> UpdateProduct(Guid id ,UpdateProductDTO updateProductDTO)
        {
            try
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
                if (product != null)
                {
                    product.Title = updateProductDTO.Title;
                    product.Description = updateProductDTO.Description;
                    product.Price = updateProductDTO.Price;
                    product.CategoryId = updateProductDTO.CategoryId;
                    product.ImagePro = (updateProductDTO.ImagePro != null) ? updateProductDTO.ImagePro : product.ImagePro;
                    product.Discount = updateProductDTO.Discount;
                    product.Quantity = updateProductDTO.Quantity;
                    product.CityId = updateProductDTO.CityId;
                    product.GenreId = updateProductDTO.GenreId;
                    product.Status = updateProductDTO.Status;
                    product.IsDisplay = updateProductDTO.IsDisplay;
                    _unitOfWork.ProductRepo.Update(product);
                    var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRatingCommentProduct(Guid id, UpdateRatingProductDTO productDTO)
        {
            try
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
                if (product != null)
                {
                    product.Rated = (productDTO.Rated != null) ? productDTO.Rated : product.Rated;
                    product.RatedCount = (productDTO.RatedCount != null) ? productDTO.RatedCount : product.RatedCount;
                    product.CommentCount = (productDTO.CommentCount != null) ? productDTO.CommentCount : product.CommentCount;
                    _unitOfWork.ProductRepo.Update(product);
                    var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatusProduct(Guid id, UpdateStatusProductDTO productDTO)
        {
            try
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
                if (product != null)
                {
                    product.DenyRes = productDTO.DenyRes;
                    product.Status = productDTO.Status;
                    product.IsDisplay = productDTO.IsDisplay;
                    _unitOfWork.ProductRepo.Update(product);
                    var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
