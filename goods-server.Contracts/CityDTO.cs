namespace goods_server.Contracts
{
    public class CityDTO
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
    }

    public class CreateCityDTO
    {
        public string Name { get; set; }
    }

    public class UpdateCityDTO
    {
        public string Name { get; set; }
    }
}
