using Clientes.Business.Models;
using Responses;

namespace Clientes.Api.Dto.Response;

public class ClientesPaginatedResponse : PaginatedResponse
{
    /// <summary>
    /// Lista de candidatos
    /// </summary>
    public IEnumerable<Cliente> Clientes { get; private set; }

    public ClientesPaginatedResponse(int totalRecords,
        int pageNumber,
        int pageCount,
        IEnumerable<Cliente> clientes) :
        base(totalRecords, pageNumber, pageCount, clientes.Count()) => Clientes = clientes;

}
