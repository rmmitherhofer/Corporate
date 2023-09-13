using System.Text.Json.Serialization;

namespace PanPan.WebApp.Models.Response;

/// <summary>
/// Classe de resposta de validações
/// </summary>
public class ValidationResponse
{
    /// <summary>
    /// Notificações
    /// </summary>
    [JsonPropertyName("validations")]
    public IEnumerable<NotificationResponse> Validations { get; set; }

}