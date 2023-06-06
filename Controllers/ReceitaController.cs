using Microsoft.AspNetCore.Mvc;

public class ReceitaController : Controller
{
    IReceitaRepository receitaRepository;

    public ReceitaController(IReceitaRepository receitaRepository)
    {
        this.receitaRepository = receitaRepository;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Receita receita)
    {
        receitaRepository.Create(receita);
        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        Receita receita = receitaRepository.Read(id);
        if (receita != null)
        {
            return View(receita);
        }
        return NotFound();
    }
    
    public IActionResult Delete(int id)
    {
        receitaRepository.Delete(id);
        return RedirectToAction("Index","Home");
    }

    [HttpPost]
    public IActionResult Update(Receita receita, int id)
    {
        receitaRepository.Update(receita, id);
        return RedirectToAction("Index","Home");
    }

}