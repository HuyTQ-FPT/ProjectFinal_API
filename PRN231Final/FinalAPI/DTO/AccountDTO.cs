namespace FinalAPI.DTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public int? IsReport { get; set; }
    }
}
