using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ItemController : Controller
    {
        private readonly Webapp _webapp;
        public ItemController(Webapp webapp){
            _webapp = webapp;
       }
        public async Task<IActionResult> Index()
        {
         
            var item = await _webapp.Items.ToListAsync();
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id","Name","Price")] Item item)
        {
            bool itemexists =  await _webapp.Items.AnyAsync(x => x.Name == item.Name);
            if (itemexists) {
                ModelState.AddModelError("Name", "An item alreday exist");
                return View(item);
            }

            if (ModelState.IsValid) {
                
                _webapp.Items.Add(item); //Adds item to table memory
                await _webapp.SaveChangesAsync();// saves it in database
                return RedirectToAction("Index");
            }
            return View(item);
            
        }
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _webapp.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id","Name","Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _webapp.Update(item);
                await _webapp.SaveChangesAsync();
                return RedirectToAction("Index");   
            }
            return View(item);
        }
        public async Task<ActionResult> Delete( int id)
        {
            var item = await _webapp.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost , ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var item = await _webapp.Items.FindAsync(id);
            if (item != null)
            {
                _webapp.Items.Remove(item);
                await _webapp.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
   
    }
}
