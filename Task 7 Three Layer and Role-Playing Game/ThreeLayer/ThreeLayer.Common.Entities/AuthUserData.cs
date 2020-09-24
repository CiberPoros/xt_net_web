namespace ThreeLayer.Common.Entities
{
    public class AuthUserData : IEntityWithId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
