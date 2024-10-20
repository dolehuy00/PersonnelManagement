namespace PersonnelManagement.DTO.Filter
{
    public class BaseFilter
    {
        public int Page { get; set; } = 1;  // Số trang, mặc định là 1
        public int PageSize { get; set; } = 10;   // Kích thước trang, mặc định là 10
        public String SortBy { get; set; } = "";
    }
}
