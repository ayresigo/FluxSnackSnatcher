namespace FluxSnackSnatcher.Models
{
    public class AccountData
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int GroupId { get; set; }

        public string Server { get; set; } = null!;

        public DateTime Date { get; } = DateTime.Now;
    }
}
