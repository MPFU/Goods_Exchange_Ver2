using goods_server.Contracts;
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
        
        public Task<bool> DeleteProduct(Guid id);
    }
}
