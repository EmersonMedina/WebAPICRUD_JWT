namespace WebAPICRUD_JWT.Dtos
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }

        public RegisterUserDTO()
        {
            CreationDate = DateTime.Now;
            Active = true;
        }
    }
}
