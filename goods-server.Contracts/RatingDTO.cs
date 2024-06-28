namespace goods_server.Contracts
{
    public class RatingDTO
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Descript { get; set; }
        public int? Rated { get; set; }
    }

    public class CreateRatingDTO
    {
        public Guid? CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string? Descript { get; set; }
        public int? Rated { get; set; }
    }

    public class UpdateRatingDTO
    {
        public string? Descript { get; set; }
        public int? Rated { get; set; }
    }
}
