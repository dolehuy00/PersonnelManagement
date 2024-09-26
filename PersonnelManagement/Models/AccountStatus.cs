namespace PersonnelManagement.Model
{
    public class AccountStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
