using PanPan.WebApp.Models.Request.Vaga;
using PanPan.WebApp.Models.ViewModels.Vagas;
using Zypher.Http;
using Zypher.Http.Extensions;
using Zypher.Json;
using Zypher.Notifications.Interfaces;

namespace PanPan.WebApp.HttpServices;

public interface IVagaHttpService
{
    Task<IEnumerable<VagaViewModel>> ObterTodos();
    Task Adicionar(AddVagaRequest request);
    Task Alterar(UpdateVagaRequest request);
}
public class VagaHttpService : HttpService, IVagaHttpService
{
    public VagaHttpService(HttpClient httpClient, INotificationHandler notificationHandler, ILogger<VagaHttpService> logger) : base(httpClient, notificationHandler, logger) { }

    public async Task<IEnumerable<VagaViewModel>> ObterTodos()
    {
        var uri = "api/v1/vaga";

        var response = await GetAsync(uri);

        response.HasErrors();

        return await response.ReadAsAsync<IEnumerable<VagaViewModel>>();
    }

    public async Task Adicionar(AddVagaRequest request)
    {
        var uri = "api/v1/vaga";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PostAsync(uri, content);

        response.HasErrors();
    }

    public async Task Alterar(UpdateVagaRequest request)
    {
        var uri = "api/v1/vaga";

        var content = JsonExtensions.SerializeContent(request);

        var response = await PutAsync(uri, content);

        response.HasErrors();
    }
}