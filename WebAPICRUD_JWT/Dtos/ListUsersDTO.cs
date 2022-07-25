namespace WebAPICRUD_JWT.Dtos
{
    public class ListUsersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
    }
}
