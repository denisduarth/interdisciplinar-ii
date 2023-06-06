using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class UsuarioController : Controller
{
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        this._usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        _usuarioRepository.Create(usuario);
        return View("/Views/Login/Index.cshtml");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        Usuario usuario = _usuarioRepository.Read(id);
        if(usuario != null)
        {
            return View(usuario);
        }
        return NotFound();
    }

    public IActionResult Delete(int id)
    {
        Logout();
        _usuarioRepository.Delete(id);
        return View("/Views/Login/Index.cshtml");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        Usuario usuario = _usuarioRepository.Read(id);
        if(usuario != null)
        {
            return View(usuario);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Update(Usuario usuario, int id)
    {
        _usuarioRepository.Update(usuario, id);
        return View("/Views/Home/Index.cshtml");
    }

    [HttpPost]
    public IActionResult Login(IFormCollection form)
    {
        string? email = form["email"];
        string? password = form["password"];

        Usuario? user = _usuarioRepository.Login(email!, password!);

        if(user == null)
        {
            ViewBag.Error = "Usuário/Senha inválidos";
            return View("Index","Login");
        }

        HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));

        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index","Login");
    }
}