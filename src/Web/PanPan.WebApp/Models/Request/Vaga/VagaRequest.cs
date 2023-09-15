namespace PanPan.WebApp.Models.Request.Vaga;

public class AddVagaRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Guid ClienteId { get; set; }
}

public class UpdateVagaRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}
