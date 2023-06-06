using Microsoft.AspNetCore.Mvc;

public class CategoriaController : Controller
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        this._categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Categoria> categorias = _categoriaRepository.Read(); 
        return View(categorias);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Categoria categoria)
    {
        _categoriaRepository.Create(categoria);
        return View("Index");
    }

    public IActionResult Delete(int id)
    {
        _categoriaRepository.Delete(id);
        return View("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        Categoria categoria = _categoriaRepository.Read(id);
        if(categoria != null)
        {
            return View(categoria);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Update(Categoria categoria, int id)
    {
        _categoriaRepository.Update(categoria, id);
        return View("Index");
    }
}