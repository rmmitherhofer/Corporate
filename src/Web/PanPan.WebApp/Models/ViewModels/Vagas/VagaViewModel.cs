namespace PanPan.WebApp.Models.ViewModels.Vagas;

public class VagaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Guid ClienteId { get; set; }
}
