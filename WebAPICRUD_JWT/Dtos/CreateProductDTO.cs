namespace WebAPICRUD_JWT.Dtos
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }

        public CreateProductDTO()
        {
            CreationDate = DateTime.Now;
            Active = true; 
        }
    }
}
