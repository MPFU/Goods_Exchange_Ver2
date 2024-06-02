namespace goods_server.Contracts
{
    public class RatingDTO
    {
        public Guid ProductId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }

    public class CreateRatingDTO
    {
        public Guid ProductId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateRatingDTO
    {
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
