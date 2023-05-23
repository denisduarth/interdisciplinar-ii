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
    public IActionResult Create(Receita receita, Categoria categoria)
    {
        receitaRepository.Create(receita, categoria);
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
    
    [HttpDelete]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        receitaRepository.Delete(id);
        return RedirectToAction("Index","Home");
    }

    [HttpPut]
    [HttpPost]
    public IActionResult Update(Receita receita, int id)
    {
        receitaRepository.Update(receita, id);
        return RedirectToAction("Index","Home");
    }

}