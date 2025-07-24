using PanPan.WebApp.Models.Response.NewFolder;

namespace PanPan.WebApp.HttpServices;

public interface ITalentMindHttpService
{
    Task<ParserHttpResponse> ExtractFromPdf(IFormFile file);
}
