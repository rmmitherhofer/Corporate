namespace PanPan.WebApp.Models.Response;

public abstract class PaginatedResponse
{
    public int TotalRecords { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int PageCount { get; set; }
    public int? BackPage { get; set; }
    public int? NextPage { get; set; }
}
