namespace Vagas.Api.Dto.Request;

public class AddVagaRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Guid ClienteId { get; set; }
}

public class UpdatteVagaRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; } 
}
