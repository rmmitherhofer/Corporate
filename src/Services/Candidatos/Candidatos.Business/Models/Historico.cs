using Core.DomainObjects;
using System.Text.Json.Serialization;

namespace Candidatos.Business.Models;

public class Historico : Entity
{
    public Guid CandidatoId { get; private set; }
    public string? Comentario { get; private set; }

    [JsonIgnore]
    public Candidato Candidato { get; protected set; }
    public Historico(Guid candidatoId, string? comentario)
    {
        CandidatoId = candidatoId;
        Comentario = comentario;
    }
}
