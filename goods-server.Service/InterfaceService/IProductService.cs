﻿using goods_server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IProductService
    {
        public Task<GetProductDTO> GetProduct(Guid id);
        public Task<bool> CreateProduct(CreateProductDTO createProductDTO);
        public Task<bool> UpdateProduct(Guid id, UpdateProductDTO updateProductDTO);
        public Task<bool> DeleteProduct(Guid id);
        public Task<bool> UpdateRatingCommentProduct(Guid id, UpdateRatingProductDTO productDTO);
        public Task<bool> UpdateStatusProduct(Guid id, UpdateStatusProductDTO productDTO);
    }
}