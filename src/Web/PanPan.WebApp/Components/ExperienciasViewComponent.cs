using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models;

namespace PanPan.WebApp.Components;

public class ExperienciasViewComponent : ViewComponent
{

    private readonly ICandidatoHttpService _httpService;
    private readonly IMapper _mapper;

    public ExperienciasViewComponent(ICandidatoHttpService httpService, IMapper mapper)
    {
        _httpService = httpService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid candidatoId)
    {
        var response = await _httpService.ObterExperiencias(candidatoId);

        var model = _mapper.Map<IEnumerable<ExperienciaViewModel>>(response);

        return View(model);
    }
}