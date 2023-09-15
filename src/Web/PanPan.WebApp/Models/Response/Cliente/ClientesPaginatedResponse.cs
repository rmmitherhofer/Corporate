namespace PanPan.WebApp.Models.Response.Cliente;

public class ClientesPaginatedResponse : PaginatedResponse
{
    public IEnumerable<ClienteResponse> Clientes { get; set; }
}
