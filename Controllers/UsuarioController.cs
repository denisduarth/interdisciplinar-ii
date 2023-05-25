using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class UsuarioController : Controller
{
    IUsuarioRepository usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
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
        usuarioRepository.Create(usuario);
        return View("/Views/Login/Index.cshtml");
    }

    public IActionResult Delete(int id)
    {
        usuarioRepository.Delete(id);
        return View("Index", "Login");
    }

    [HttpPost]
    public IActionResult Update(int id)
    {
        Usuario usuario = usuarioRepository.Read(id);
        if(usuario != null)
        {
            return View(usuario);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Update(Usuario usuario, int id)
    {
        usuarioRepository.Update(usuario, id);
        return View("Index","Home");
    }

    [HttpPost]
    public ActionResult Login(IFormCollection form)
    {
        string? email = form["email"];
        string? password = form["password"];

        var user = usuarioRepository.Login(email!, password!);

        if(user == null)
        {
            ViewBag.Error = "Usuário/Senha inválidos";
            return View();
        }

        HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));

        return RedirectToAction("/Views/Home/Index.cshtml");
    }

    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("/Views/Login/Index.cshtml");
    }


}