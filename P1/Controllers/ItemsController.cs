using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModels;
using LogicModels;
using Newtonsoft.Json;

namespace P1.Controllers
{
    public class ItemsController : Controller
    {
        private readonly Context _context;

        private readonly Itemz _itemz;

        private readonly Inventoryz _inventoryz;

        Cart cart = new();

        public ItemsController(Context context, IItemz itemz, IInventoryz inventoryz)
        {
            _context = context;
            _itemz = (Itemz)itemz;
            _inventoryz = (Inventoryz)inventoryz;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        public IActionResult ShowSelected(string Item)
        {
            string _item = JsonConvert.DeserializeObject<string>(Item);
            List<Item> showitem = _itemz.ShowItems(_item);
            List<decimal> price = new();
            List<string> whatis = new();
            List<int> quantity = new();
            int storeid = 1;
            for (int i = 0; i<showitem.Count(); i++)
                {
                price.Add(showitem[i].Price);
                quantity.Add(_inventoryz.ShowQuantity(showitem[i].Itemid, storeid));
                whatis.Add(showitem[i].Descriptionforconsole);
            }
            ViewBag.Price = price;
            ViewBag.Amount = quantity;
            ViewBag.Whatis = whatis;
            ViewBag.Thing = showitem;
            return View(_context);
        }

        public IActionResult AddtoCart(int[] quant, string[] product )
        {
            
            for(int i=0; i<quant.Length; i++)
            {
                cart.Cartadd(quant[i], product[i]);
            }
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Cart()
        {
            List<string> Item = new();
            List<int> Quant = new();
            foreach (KeyValuePair<string, int> thing in cart.FinalCart())
            {
                Item.Add(thing.Key);
                Quant.Add(thing.Value);
            }
            ViewBag.Item = Item;
            ViewBag.Quant = Quant;
            return View(_context);
        }



        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .FirstOrDefaultAsync(m => m.Itemid == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Itemid,Item1,Price,Descriptionforconsole")] Item items)
        {
            if (ModelState.IsValid)
            {
                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Itemid,Item1,Price,Descriptionforconsole")] Item items)
        {
            if (id != items.Itemid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.Itemid))
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
            return View(items);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .FirstOrDefaultAsync(m => m.Itemid == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.Items.FindAsync(id);
            _context.Items.Remove(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.Itemid == id);
        }
    }
}
