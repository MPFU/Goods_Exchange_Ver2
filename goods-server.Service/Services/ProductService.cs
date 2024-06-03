using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.FilterModel;
using goods_server.Service.FilterModel.Helper;
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

        public async Task<PagedResult<GetProductDTO>> GetAllProductAsync<T>(ProductFilter productFilter)
        {
            var proList = _mapper.Map<IEnumerable<GetProductDTO>>(await _unitOfWork.ProductRepo.GetAllProductAsync());
            IQueryable<GetProductDTO> filterPo = proList.AsQueryable();

            //Filtering

            if (!string.IsNullOrEmpty(productFilter.Title))
                filterPo = filterPo.Where(x => x.Title.Contains(productFilter.Title, StringComparison.OrdinalIgnoreCase));

            if (productFilter.CreatorId != null)
                filterPo = filterPo.Where(x => x.CreatorId.Equals(productFilter.CreatorId));

            if (productFilter.Price != null)
                filterPo = filterPo.Where(x => x.Price ==  productFilter.Price);

            if (productFilter.Rated != null)
                filterPo = filterPo.Where(x => x.Rated == productFilter.Rated);

            if (!string.IsNullOrEmpty(productFilter.Status))
                filterPo = filterPo.Where(x => x.Status.Contains(productFilter.Status, StringComparison.OrdinalIgnoreCase));
            
            // Sorting
            if (!string.IsNullOrEmpty(productFilter.SortBy))
            {
                switch (productFilter.SortBy)
                {
                    //case "userName":
                    //    filterPo = accountFilter.SortAscending ?
                    //        filterAcc.OrderBy(x => x.UserName) :
                    //        filterAcc.OrderByDescending(x => x.UserName);
                    //    break;
                    //case "joinDate":
                    //    filterAcc = accountFilter.SortAscending ?
                    //        filterAcc.OrderBy(x => x.JoinDate) :
                    //        filterAcc.OrderByDescending(x => x.JoinDate);
                    //    break;
                    default:
                        filterPo = productFilter.SortAscending ?
                            filterPo.OrderBy(item => GetProperty.GetPropertyValue(item, productFilter.SortBy)) :
                            filterPo.OrderByDescending(item => GetProperty.GetPropertyValue(item, productFilter.SortBy));
                        break;

                }
            }

            // Paging
            var pageItems = filterPo
                .Skip((productFilter.PageNumber - 1) * productFilter.PageSize)
                .Take(productFilter.PageSize)
                .ToList();

            return new PagedResult<GetProductDTO>
            {
                Items = pageItems,
                PageNumber = productFilter.PageNumber,
                PageSize = productFilter.PageSize,
                TotalItem = proList.Count(),
                TotalPages = (int)Math.Ceiling((decimal)proList.Count() / (decimal)productFilter.PageSize)
            };
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
