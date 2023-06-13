using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Interdisciplinar.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Interdisciplinar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IReceitaRepository _receitaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    [ActivatorUtilitiesConstructor]
    public HomeController(
            ILogger<HomeController> logger, 
            IReceitaRepository receitaRepository,
            IUsuarioRepository usuarioRepository, 
            ICategoriaRepository categoriaRepository, 
            IHttpContextAccessor contextAccessor)
    {
        this._logger = logger;
        this._receitaRepository = receitaRepository;
        this._usuarioRepository = usuarioRepository;
        this._categoriaRepository = categoriaRepository;
        this._httpContextAccessor = contextAccessor;
    }

    [HttpGet]
    public IActionResult Index()
    {
        Random random = new Random();
        Usuario? user = null;

        List<Categoria> categorias = _categoriaRepository.Read();
        ViewData["Categorias"] = categorias;

        string? session = _httpContextAccessor?.HttpContext?.Session.GetString("user");
        if (!string.IsNullOrEmpty(session))
        {
            user = JsonSerializer.Deserialize<Usuario>(session);
        }

        List<Receita> receitasRepository = _receitaRepository.Read();
        List<Receita> todasAsReceitas = receitasRepository;

        if (user != null)
        {
            List<Receita> receitasUsuario = _receitaRepository.GetByUsuarioId(user.idUsuario);
            todasAsReceitas = receitasRepository.Concat(receitasUsuario).ToList();
            todasAsReceitas = todasAsReceitas.OrderBy(r => random.Next()).ToList();
        }

        session = JsonSerializer.Serialize(user);
        _httpContextAccessor.HttpContext?.Session.SetString("user", session);

        return View(todasAsReceitas);
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
