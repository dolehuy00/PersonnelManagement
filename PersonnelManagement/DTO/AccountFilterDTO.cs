namespace PersonnelManagement.DTO
{
    public class AccountFilterDTO
    {
        public string? Keyword { get; set; }
        public string? SortByEmail { get; set; }
        public int? FilterByStatus { get; set; }
        public int? FilterByRole { get; set; }
        public string? KeywordByEmployee { get; set; }
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
