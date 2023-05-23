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
        return View("Index", "Login");
    }

    public IActionResult Delete(int id)
    {
        usuarioRepository.Delete(id);
        return View("Index", "Login");
    }

    // public IActionResult Update(int id)
    // {
    //     var usuario = usuarioRepository.Read(id);
    // }

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

        return RedirectToAction("Index", "Home");
    }

}