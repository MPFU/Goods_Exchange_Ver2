namespace goods_server.Contracts
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class CreateCategoryDTO
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryDTO
    {
        public string Name { get; set; }
    }
}
