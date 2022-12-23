using AppWeb.Data;
using AppWeb.Models;
using AppWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DataContext datacontext;

        public UsuariosController(DataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        [HttpGet]
        public async Task<IActionResult>  Index() 
        {
            var usuarios = await datacontext.Usuarios.ToListAsync();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddVMUsuario addUsuarioRequest)
        {
            var ususario = new Usuario()
            {
                Id = Guid.NewGuid(),
                Name = addUsuarioRequest.Name,
                Email = addUsuarioRequest.Email,
                Salario = addUsuarioRequest.Salario,
                Edad = addUsuarioRequest.Edad,
                Fecha = addUsuarioRequest.Fecha
            };
            await datacontext.Usuarios.AddAsync(ususario);
            await datacontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> view(Guid id)
        {
            var usuario = await datacontext.Usuarios.FirstOrDefaultAsync(v => v.Id == id);

            if(usuario != null)
            {
                var viewModel = new EditUsuarioM()
                {
                    Id = usuario.Id,
                    Name = usuario.Name,
                    Email = usuario.Email,
                    Salario = usuario.Salario,
                    Edad = usuario.Edad,
                    Fecha = usuario.Fecha
                };
                return await Task.Run(() => View("view", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> view(EditUsuarioM model)
        {
            var usuario = await datacontext.Usuarios.FindAsync(model.Id);

            if(usuario != null) 
            {
                usuario.Name = model.Name;
                usuario.Email = model.Email;
                usuario.Salario = model.Salario;
                usuario.Edad = model.Edad;
                usuario.Fecha = model.Fecha;

                await datacontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public  async Task<IActionResult> Borrar(EditUsuarioM model)
        {
            var usuario = await datacontext.Usuarios.FindAsync(model.Id);
            if(usuario != null)
            {
                datacontext.Usuarios.Remove(usuario);
                await datacontext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");   
        }



    }
}
