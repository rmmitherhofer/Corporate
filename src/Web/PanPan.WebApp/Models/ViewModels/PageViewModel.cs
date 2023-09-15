namespace PanPan.WebApp.Models.ViewModels;

public abstract class PageViewModel : ViewModel
{
    public int TotalRecords { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public int PageCount { get; set; }
    public int? BackPage { get; set; }
    public int? NextPage { get; set; }
    public string OrderBy { get; set; }
}
