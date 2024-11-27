namespace PersonnelManagement.DTO.Filter
{
    public class AccountFilterDTO
    {
        public string? Keyword { get; set; }
        public string? SortByEmail { get; set; }
        public string? FilterByStatus { get; set; }
        public string? FilterByRole { get; set; }
        public string? KeywordByEmployee { get; set; }
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
