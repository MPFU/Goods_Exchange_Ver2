using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class ProductDTO
    {

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImagePro { get; set; }

        public Guid? CreatorId { get; set; }

        public decimal? Price { get; set; }

        public Guid? CategoryId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Rated { get; set; }

        public int? RatedCount { get; set; }

        public int? CommentCount { get; set; }

        public int? Discount { get; set; }

        public int? Quantity { get; set; }

        public Guid? CityId { get; set; }

        public string? DenyRes { get; set; }

        public string? Status { get; set; }

        public Guid? GenreId { get; set; }

        public string? IsDisplay { get; set; }

    }

    public class GetProductDTO : ProductDTO
    {
        public Guid ProductId { get; set; }
    }

    public class GetProduct2DTO : ProductDTO
    {
        public Guid ProductId { get; set; }
        public CategoryDTO? Category { get; set; }
        public CityDTO? City { get; set; }
        public GenreDTO? Genre { get; set; }
    }

    public class CreateProductDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImagePro { get; set; }

        public Guid? CreatorId { get; set; }

        public decimal? Price { get; set; }

        public Guid? CategoryId { get; set; }

        public int? Discount { get; set; }

        public int? Quantity { get; set; }

        public Guid? CityId { get; set; }

        public string? Status { get; set; }

        public Guid? GenreId { get; set; }

        public string? IsDisplay { get; set; }
    }

    public class UpdateProductDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImagePro { get; set; }

        public decimal? Price { get; set; }

        public Guid? CategoryId { get; set; }

        public int? Discount { get; set; }

        public int? Quantity { get; set; }

        public Guid? CityId { get; set; }

        public string? Status { get; set; }

        public Guid? GenreId { get; set; }

        public string? IsDisplay { get; set; }
    }

    public class UpdateRatingProductDTO
    {
        public int? Rated { get; set; }

        public int? RatedCount { get; set; }

        public int? CommentCount { get; set; }
    }

    public class UpdateStatusProductDTO
    {
        public string? DenyRes { get; set; }

        public string? Status { get; set; }

        public string? IsDisplay { get; set; }
    }
}
