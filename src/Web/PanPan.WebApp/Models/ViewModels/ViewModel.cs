namespace PanPan.WebApp.Models.ViewModels;

public abstract class ViewModel { }

public class ErrorViewModel : ViewModel
{
    public int ErrorCode { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string? CorrelationId { get; set; }
}
