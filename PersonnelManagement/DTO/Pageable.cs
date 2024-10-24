namespace PersonnelManagement.DTO
{
    public class Pageable
    {
        public int Page { get; set; } = 1;  // Số trang, mặc định là 1
        public int PageSize { get; set; } = 10;   // Kích thước trang, mặc định là 10
        public String SortBy { get; set; } = "";
    }
}
