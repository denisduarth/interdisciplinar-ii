using Microsoft.AspNetCore.Mvc;

public class ReceitaController : Controller
{
    private readonly IReceitaRepository _receitaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public ReceitaController(IReceitaRepository receitaRepository, ICategoriaRepository categoriaRepository)
    {
        this._receitaRepository = receitaRepository;
        this._categoriaRepository = categoriaRepository;
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
        _receitaRepository.Create(receita);
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

    [HttpPost]
    public IActionResult Update(Receita receita, int id)
    {
        _receitaRepository.Update(receita, id);
        return RedirectToAction("Index","Home");
    }

}