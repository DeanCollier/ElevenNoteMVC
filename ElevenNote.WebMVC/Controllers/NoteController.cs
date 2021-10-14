using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // connect to services for db
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
        // GET: Note
        public async Task<ActionResult> Index()
        {
            var service = CreateNoteService();

            var model = await service.GetNotesAsync();

            return View(model);
        }

        // GET: Create Note
        // Note/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }
        // POST: Create Note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoteCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreateNoteService();

                if (await service.CreateNoteAsync(model))
                {
                    TempData["SaveResult"]= "Your note was created.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Note could not be created.");
                return View(model);
            }
            return View(model);
        }

        

    }
}