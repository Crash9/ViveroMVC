using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViveroMVC.Models;

namespace ViveroMVC.Controllers
{
    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Plant Plant { get; set; }
        public PlantsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Plant = new Plant();
            if (id == null) 
            {
                //create
                return View(Plant);
            }
            //update
            Plant = _db.Plants.FirstOrDefault(v => v.Id == id);
            if (Plant == null)
            {
                return NotFound();
            }
            return View(Plant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Plant.Id == 0)
                {
                    _db.Plants.Add(Plant);
                }
                else
                {
                    _db.Plants.Update(Plant);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Plant);
        }
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Plants.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var plantFromDb = await _db.Plants.FirstOrDefaultAsync(v => v.Id == id);
            if (plantFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Plants.Remove(plantFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
