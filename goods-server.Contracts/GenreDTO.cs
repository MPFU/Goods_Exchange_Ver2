namespace goods_server.Contracts
{
    public class GenreDTO
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
    }

    public class CreateGenreDTO
    {
        public string Name { get; set; }
    }

    public class UpdateGenreDTO
    {
        public string Name { get; set; }
    }
}
