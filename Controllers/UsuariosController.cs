
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4U.Models;
using Task4U.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class UsuariosController : Controller
{
    private readonly TskDbContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public UsuariosController(TskDbContext context, IPasswordHasher<Usuario> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
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

        // Se o usuário não existir, já rejeita imediatamente
        if (usuario == null)
        {
            ViewBag.Erro = "Usuário ou senha inválidos.";
            return View("Login"); // CORREÇÃO: return View mantém a ViewBag viva na tela
        }

        // 2. VALIDAÇÃO DA SENHA: Compara a senha digitada com o Hash do banco de dados
        var resultadoSenha = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha ?? "", senha);

        // Se a senha estiver errada ou for inválida
        if (resultadoSenha == PasswordVerificationResult.Failed)
        {
            ViewBag.Erro = "Usuário ou senha inválidos.";
            return View("Login"); 
        }
        
        // Se o usuário estiver inativo no sistema, bloqueia o acesso
        if (!usuario.Ativo)
        {
            ViewBag.Erro = "Esta conta de usuário está desativada.";
            return View("Login");
        }

        // 2. Define o que será guardado CRIPTOGRAFADO dentro do Cookie
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Nivel.ToString()) 
        };

        var identidadeUsuario = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 3. Efetua o login salvando o Cookie criptografado no navegador do usuário
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identidadeUsuario)
        );

            // Atualiza os dados de auditoria do último acesso (Opcional, mas recomendado)
        usuario.UltimoAcessoData = DateTime.UtcNow;
        usuario.UltimoAcessoIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP desconhecido";
        _context.Entry(usuario).Property(u => u.UltimoAcessoData).IsModified = true;
        _context.Entry(usuario).Property(u => u.UltimoAcessoIP).IsModified = true;
        await _context.SaveChangesAsync();

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
    // [HttpPost]
    // // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Create([Bind("Id,IncData,IncUsuarioId,AltData,AltUsuarioId,Nome,Email,Senha,UltimoAcessoData,UltimoAcessoIP,Nivel,Ativo")] Usuario usuario)
    // {
    //     if (ModelState.IsValid)
    //     {
            
    //         usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha); // Hash da senha
    //         _context.Add(usuario);
    //         await _context.SaveChangesAsync();
    //         return RedirectToAction(nameof(Index));
    //     }
    //     return View(usuario);
    // }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Nome,Email,Senha")] Usuario usuario)
{
    if (ModelState.IsValid)
    {
        // 1. Correção do erro: Acessa a propriedade correta do objeto 'usuario'
        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha ?? ""); 

        // 2. Segurança e Regra de Negócio: Preenche os campos de controle no servidor
        usuario.IncData = DateTime.Now;

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int usuarioLogadoId))
        {
            usuario.IncUsuarioId = usuarioLogadoId;
        }

        usuario.Ativo = true;
        usuario.Nivel = (NivelUsuario)1; // Nível padrão (ex: Usuário Comum)

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
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(int? id, [Bind("Id,IncData,IncUsuarioId,AltData,AltUsuarioId,Nome,Email,UltimoAcessoData,UltimoAcessoIP,Nivel,Ativo")] Usuario usuario)
    // {
    //     if (id != usuario.Id)
    //     {
    //         return NotFound();
    //     }

    //     if (ModelState.IsValid)
    //     {
    //         try
    //         {

    //             var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //             if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int usuarioLogadoId))
    //             {
    //                 usuario.AltUsuarioId = usuarioLogadoId;
    //             }

    //             _context.Update(usuario);
    //             _context.Entry(usuario).Property(u => u.Senha).IsModified = false; // Evita atualizar a senha
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!UsuarioExists(usuario.Id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }

    //         return RedirectToAction(nameof(Index));
    //     }

    //     return View(usuario);
    // }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Nivel,Ativo")] Usuario usuario)
    {
        // Removi campos de auditoria do Bind por segurança, controlamos eles aqui dentro
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // 1. CORREÇÃO: Recupera e declara a variável 'userIdClaim' a partir do Cookie
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int usuarioLogadoId))
                {
                    usuario.AltUsuarioId = usuarioLogadoId;
                }

                // Define a data de alteração atualizada para o PostgreSQL
                usuario.AltData = DateTime.UtcNow;

                // 2. Anexa a entidade ao rastreamento do EF
                _context.Update(usuario);

                // 3. SEGURANÇA: Impede o EF de sobrescrever/zerar a senha original
                _context.Entry(usuario).Property(u => u.Senha).IsModified = false; 

                // 4. SEGURANÇA: Impede o EF de apagar quem criou o registro e quando criou
                _context.Entry(usuario).Property(u => u.IncData).IsModified = false;
                _context.Entry(usuario).Property(u => u.IncUsuarioId).IsModified = false;
                
                // Se as colunas de último acesso não estiverem na View, proteja-as também:
                _context.Entry(usuario).Property(u => u.UltimoAcessoData).IsModified = false;
                _context.Entry(usuario).Property(u => u.UltimoAcessoIP).IsModified = false;

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

    // TODO: APAGAR ESTE MÉTODO APÓS RODAR UMA VEZ NO NAVEGADOR
// [HttpGet]
// public async Task<IActionResult> MigrarSenhasAntigas()
// {
//     // 1. Busca apenas os usuários que possuem a senha antiga em texto limpo "123"
//     var usuariosAntigos = await _context.Usuario
//         .Where(u => u.Senha == "123")
//         .ToListAsync();

//     int totalAtualizado = 0;

//     foreach (var usuario in usuariosAntigos)
//     {
//         // 2. Aplica a criptografia do PasswordHasher na senha antiga "123"
//         usuario.Senha = _passwordHasher.HashPassword(usuario, "123");
        
//         // Dados de auditoria opcionais para o registro
//         usuario.AltData = DateTime.UtcNow;

//         totalAtualizado++;
//     }

//     // 3. Salva todas as alterações de uma vez no PostgreSQL
//     if (totalAtualizado > 0)
//     {
//         await _context.SaveChangesAsync();
//     }

//     return Content($"Sucesso! {totalAtualizado} usuários tiveram a senha '123' criptografada.");
// }

}
