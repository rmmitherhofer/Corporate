using Candidatos.Business.Models;
using Responses;

namespace Candidatos.Api.DTO.Responses;

public class CandidatosPaginatedResponse : PaginatedResponse
{
    /// <summary>
    /// Lista de candidatos
    /// </summary>
    public IEnumerable<Candidato> Candidatos { get; private set; }

    public CandidatosPaginatedResponse(int totalRecords, 
        int pageNumber, 
        int pageCount, 
        IEnumerable<Candidato> candidatos) : 
        base(totalRecords, pageNumber, pageCount, candidatos.Count()) => Candidatos = candidatos;

}
