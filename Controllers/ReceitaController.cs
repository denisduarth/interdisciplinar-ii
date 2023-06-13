using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;


public class ReceitaController : Controller
{
    public Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;
    private readonly IReceitaRepository _receitaRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public ReceitaController(
        IReceitaRepository receitaRepository, 
        ICategoriaRepository categoriaRepository, 
        IUsuarioRepository usuarioRepository,
        IHttpContextAccessor contextAccessor)
    {
        this._receitaRepository = receitaRepository;
        this._categoriaRepository = categoriaRepository;
        this._usuarioRepository = usuarioRepository;
        this.HttpContextAccessor = contextAccessor;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Receita> receitas = _receitaRepository.Read();
        return View(receitas);
    }

    [HttpGet]
    public IActionResult Create()
    {
        List<Categoria> categorias = _categoriaRepository.Read();
        return View(categorias);
    }

    [HttpPost]
    public IActionResult Create(Receita receita)
    {
        Usuario? user = null;

        string? session = HttpContextAccessor.HttpContext?.Session.GetString("user");
        if (session != null)
        {
            user = JsonSerializer.Deserialize<Usuario>(session);
        }


        if (user != null)
        {
            _receitaRepository.Create(receita, user.idUsuario);
            
            user.receitasCriadas.Add(receita);
            string updatedSession = JsonSerializer.Serialize(user);
            HttpContextAccessor.HttpContext?.Session.SetString("user", updatedSession);
        }

        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        Receita receita = _receitaRepository.Read(id);

        if (receita != null)
        {
            return View(receita);
        }
        return NotFound();
    }
    
    public IActionResult Delete(int id)
    {
        _receitaRepository.Delete(id);
        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        Receita receita = _receitaRepository.Read(id);

        if (receita != null)
        {
            return View(receita);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Update(Receita receita, int id)
    {
        _receitaRepository.Update(receita, id);
        return RedirectToAction("Details", new{id = id});
    }

    [HttpPost]
    public IActionResult Search(string busca)
    {
        List<Receita> receitasEncontradas = _receitaRepository.Search(busca);
        return View("Index", receitasEncontradas);
    }

    [HttpGet]
    public IActionResult getByCategoria(int categoriaId)
    {
        List<Receita> receitasEncontradas = _receitaRepository.GetByCategoria(categoriaId);
        return View("Index", receitasEncontradas);
    }
}