using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuncionarioCRUD.Models;
using Npgsql;
using System.Globalization;

namespace CRUD_.NET_MVC.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public IActionResult Index()
        {
            var funcionarios = _context.Funcionarios.ToList();
            return View(funcionarios);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Funcionario funcionario)
{
    if (Request.Form.TryGetValue("Salario", out var salarioStr))
    {
        // Normaliza: troca ponto por vírgula
        string salarioNormalized = salarioStr.ToString().Replace(".", ",");

        // Converte considerando cultura pt-BR
        if (decimal.TryParse(salarioNormalized, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal salario))
        {
            funcionario.Salario = salario;
        }
        else
        {
            ModelState.AddModelError("Salario", "Digite um valor numérico válido (ex: 2.000,00)");
        }
    }

    if (ModelState.IsValid)
    {
        _context.Add(funcionario);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    return View(funcionario);
}


        // GET: Funcionarios/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (Request.Form.TryGetValue("Salario", out var salarioStr))
            {
                string salarioNormalized = salarioStr.ToString().Replace(".", ",");

                if (decimal.TryParse(salarioNormalized, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal salario))
                {
                    funcionario.Salario = salario;
                }
            }

            if (ModelState.IsValid)
            {
                _context.Update(funcionario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Funcionarios/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }
    }
}