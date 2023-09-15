namespace Vagas.Api.Dto.Request;

public class VagaRequest
{
    public Guid ClienteId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    
}

public class UpdatteVagaRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; } 
}
