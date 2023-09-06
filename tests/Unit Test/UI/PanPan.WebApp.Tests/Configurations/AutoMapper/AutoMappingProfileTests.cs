using AutoMapper;
using PanPan.WebApp.Configurations.AutoMapper;

namespace PanPan.WebApp.Tests.Configurations.AutoMapper;

public class AutoMappingProfileTests
{
    [Fact(DisplayName = "Realizar auto mapeamento de Response para ViewModel deve executar com sucesso")]
    [Trait("Configurations", "AutoMapper")]
    public void DadoUmMappingResponseToViewModelProfile_QuandoMapear_EntaoConfiguracaoDeveEstarValida()
    {
        var configuration = new MapperConfiguration(options => options.AddProfile(new MappingResponseToViewModelProfile()));

        configuration.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "Realizar auto mapeamento de ViewModel para Request deve executar com sucesso")]
    [Trait("Configurations", "AutoMapper")]
    public void DadoUmMappingViewModelToRequestProfile_QuandoMapear_EntaoConfiguracaoDeveEstarValida()
    {
        var configuration = new MapperConfiguration(options => options.AddProfile(new MappingViewModelToRequestProfile()));

        configuration.AssertConfigurationIsValid();
    }
}
