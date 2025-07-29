using PanPan.WebApp.Models.Response.NewFolder;
using Zypher.Http;
using Zypher.Http.Extensions;
using Zypher.Notifications.Interfaces;

namespace PanPan.WebApp.HttpServices;

public class TalentMindHttpService : HttpService, ITalentMindHttpService
{
    public TalentMindHttpService(HttpClient httpClient, INotificationHandler notification, ILogger<TalentMindHttpService> logger) : base(httpClient, notification, logger)
    {
    }

    public async Task<ParserHttpResponse?> ExtractFromPdf(IFormFile file)
    {
        var uri = "api/v1/resumes";

        var content = file.ToMultipartFormDataContent();

        var response = await PostAsync(uri.FormatRouteWithTemplate(), content);

        return await ValidateAndReturn<ParserHttpResponse>(response);
    }
}