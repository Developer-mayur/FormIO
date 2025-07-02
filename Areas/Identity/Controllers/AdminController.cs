using FormIOProject.Areas.Identity.Data;
using FormIOProject.Areas.Identity.Model;
using FormIOProject.Areas.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormIOProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IDapper _dapper;

        public AdminController(IDapper dapper)
        {

            _dapper = dapper;
        }

        // GET: Identity/Admin
        public  IActionResult Index()
        {
            return View();
        }



        public IActionResult AddForm()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddForm(FormIO form)
        {
            var now = DateTime.UtcNow;
            form.CreatedBy = User.Identity?.Name ?? "System";
            form.ModifiedBy = User.Identity?.Name ?? "System";
            form.FormFields = form.FormFields;
            form.CreatedUtc = now;
            form.ModifiedUtc = now;
            form.VersionId = Guid.NewGuid(); // generate a new GUID for version
            form.Latest = true;              // mark this as the latest version

       

            if (ModelState.IsValid)
            {
               await _dapper.AddFormsTable(form);
                 return RedirectToAction(nameof(Index));
            }
            return View(form);
        }   
    }
}