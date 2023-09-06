using Api.Service.Models.Responses;
using Candidatos.Business.Models;

namespace Candidatos.Api.DTO.Responses;

public class CandidatoPaginatedResponse : PaginatedResponse
{
    /// <summary>
    /// Lista de candidatos
    /// </summary>
    public IEnumerable<Candidato> Candidatos { get; private set; }

    public CandidatoPaginatedResponse(int totalRecords, 
        int pageNumber, 
        int pageCount, 
        IEnumerable<Candidato> candidatos) : 
        base(totalRecords, pageNumber, pageCount, candidatos.Count()) => Candidatos = candidatos;

}
