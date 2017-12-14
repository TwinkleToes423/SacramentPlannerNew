using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlannerNew.Models;

namespace SacramentPlannerNew.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly SacramentPlannerNewContext _context;

        public SpeakersController(SacramentPlannerNewContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerNewContext = _context.Speakers.Include(s => s.Meeting);
            return View(await sacramentPlannerNewContext.ToListAsync());
        }

         // GET: Speakers
         public async Task<IActionResult> Show(int id)
         {
            var sacramentPlannerNewContext = _context.Speakers.Include(s => s.Meeting);
         var list = await sacramentPlannerNewContext.ToListAsync();
         for (int i = 0; i < list.Count; i++)
         {
            if (list[i].MeetingId != id)
               list.Remove(list[i]);
         }
            return View(list);
         }

      // GET: Speakers/Details/5
      public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .Include(s => s.Meeting)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingPrayer");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerId,FirstName,LastName,Subject,MeetingId")] Speakers speakers)
        {
            if (ModelState.IsValid)
            {
                speakers.MeetingId = Convert.ToInt32(RouteData.Values["id"]);
                _context.Add(speakers);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Meetings", new { id = speakers.MeetingId});
            }
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingPrayer", speakers.MeetingId);
            return View(speakers);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers.SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speakers == null)
            {
                return NotFound();
            }
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingPrayer", speakers.MeetingId);
            return View(speakers);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerId,FirstName,LastName,Subject,MeetingId")] Speakers speakers)
        {
            if (id != speakers.SpeakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speakers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakersExists(speakers.SpeakerId))
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
            ViewData["MeetingId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingPrayer", speakers.MeetingId);
            return View(speakers);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .Include(s => s.Meeting)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speakers = await _context.Speakers.SingleOrDefaultAsync(m => m.SpeakerId == id);
            _context.Speakers.Remove(speakers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Meetings", new { id = speakers.MeetingId});
        }

        private bool SpeakersExists(int id)
        {
            return _context.Speakers.Any(e => e.SpeakerId == id);
        }
    }
}
