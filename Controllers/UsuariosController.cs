
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4U.Models;
using Task4U.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

public class UsuariosController : Controller
{
    private readonly TskDbContext _context;

    public UsuariosController(TskDbContext context)
    {
        _context = context;
    }

    // GET: USUARIOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Usuario.ToListAsync());
    }

    // GET: USUARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuario
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: USUARIOS/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logon(string email, string senha)
    {
         var usuario = await _context.Usuario
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null || usuario.Senha != senha) // Lembrete: use hash em produção!
        {
            ViewBag.Erro = "Usuário ou senha inválidos.";
            return View();
        }

        // 2. Define o que será guardado CRIPTOGRAFADO dentro do Cookie
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var identidadeUsuario = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 3. Efetua o login salvando o Cookie criptografado no navegador do usuário
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identidadeUsuario)
        );

        return RedirectToAction("Index", "Home");

    }


    public async Task<IActionResult> Logout()
    {
        // Limpa o cookie do navegador do usuário
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Usuarios");
    }

    // POST: USUARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,IncData,IncUsuarioId,AltData,AltUsuarioId,Nome,Email,Senha,UltimoAcessoData,UltimoAcessoIP,Nivel,Ativo")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    // GET: USUARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,IncData,IncUsuarioId,AltData,AltUsuarioId,Nome,Email,Senha,UltimoAcessoData,UltimoAcessoIP,Nivel,Ativo")] Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    // GET: USUARIOS/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuario
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return BadRequest(new { success = false, message = "ID inválido." });
        }

        var usuario = await _context.Usuario.FindAsync(id);
        
        if (usuario != null)
        {
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            // Retorna um status JSON de sucesso para o AJAX atualizar a tela
            return Json(new { success = true, message = "Usuário excluído com sucesso." });
        }

        return NotFound(new { success = false, message = "Usuário não encontrado." });
    }

    //// POST: USUARIOS/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int? id)
    //{
    //    var usuario = await _context.Usuario.FindAsync(id);
    //    if (usuario != null)
    //    {
    //        _context.Usuario.Remove(usuario);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    private bool UsuarioExists(int? id)
    {
        return _context.Usuario.Any(e => e.Id == id);
    }
}
