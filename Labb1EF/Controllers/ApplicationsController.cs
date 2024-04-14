using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1EF.Data;
using Labb1EF.Models;
using System.Globalization;

namespace Labb1EF.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly NitroStoreDbContext _context;

        public ApplicationsController(NitroStoreDbContext context)
        {
            _context = context;
        }


        // GET: Applications/Filter Employee and ApplicationDateStart
        public async Task<IActionResult> Index(int? employeeId, DateTime? startDate)
        {
            // Hämtar alla Employees till lista(dropdown)
            ViewBag.Employees = _context.Employees.ToList();

            // Hämtar alla startdatum från Applications db
            var allStartDates = await _context.Applications.Select(a => a.ApplicationDateStart).Distinct().ToListAsync();

            // HashSetLista för att hålla reda på unika månader (inte upprepa samma i min dropdown)
            HashSet<int> uniqueMonths = new HashSet<int>();

            // Lista för att lägga alla startdatum i SelectListItem som Text och Value
            // används i viewen Index i foreachloop
            var startDateList = allStartDates
                .Select(d =>
                {
                    var month = d.Month;
                    var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    // Kontrollera om månaden redan har lagts till
                    if (!uniqueMonths.Contains(month))
                    {
                        uniqueMonths.Add(month); // Lägg till månaden i HashSet om den inte redan finns
                        return new SelectListItem
                        {
                            Text = monthName, // Datum som text i dopdownListan för användaren
                            Value = d.ToString("yyyy-MM-dd")
                        };
                    }
                    return null; // Returnera null för att filtrera bort dubbla månader
                })
                .Where(item => item != null) // Filtrera bort null-värdiga som blir dubbla månader i dropdownlistan
                .ToList();

            ViewBag.StartDates = startDateList;



            // Filter av EmployeeId 
            IQueryable<Application> applications = _context.Applications.Include(a => a.Employee).Include(a => a.Reason);
            if (employeeId != null)
            {
                applications = applications.Where(a => a.EmployeeId == employeeId);
            }

            // Filter av startdatum
            // Om vald startdatum är inte null, filtrera ansökningarna baserat på den valda månaden
            if (startDate != null)
            {
                applications = applications.Where(a => a.ApplicationDateStart.Month == startDate.Value.Month);
            }

            return View(await applications.ToListAsync());
        }



        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Employee)
                .Include(a => a.Reason)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

   
        // GET: Applications/Create
        public IActionResult Create()
        {
            // Lista med Employees, varje element innehåller både EmployeeId och FirstName
            // som en ny tillfällig prop för att se För och Efternamn samtidigt i selectListan
            var employees = _context.Employees.Select(e => new
            {
                e.EmployeeId,
                FullName = $"{e.FirstName} {e.LastName}" 
            }).ToList();

            // Lista med Reasons där varje element innehåller både ReasonId och ReasonTitle
            // för att kunna visa ReasonTitle istället för ID
            var reasons = _context.Reasons.Select(r => new
            {
                r.ReasonId,
                r.ReasonTitle
            }).ToList();

            // Skapa SelectList för Employees med FullName som string och EmployeeId som value
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "FullName");

            // Skapa SelectList för Reasons med ReasonTitle som text och ReasonId som value
            ViewData["ReasonId"] = new SelectList(reasons, "ReasonId", "ReasonTitle");

            return View();
        }



        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,ApplicationDateStart,ApplicationDateEnd,SubmittDate,Message,ReasonId,EmployeeId")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var employees = _context.Employees.Select(e => new
            {
                e.EmployeeId,
                FullName = $"{e.FirstName} {e.LastName}" 
            }).ToList();

            var reasons = _context.Reasons.Select(r => new
            {
                r.ReasonId,
                r.ReasonTitle
            }).ToList();

            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "FullName", application.EmployeeId);
            ViewData["ReasonId"] = new SelectList(reasons, "ReasonId", "ReasonTitle", application.ReasonId);

            return View(application);
        }


        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", application.EmployeeId);
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "ReasonId", "ReasonTitle", application.ReasonId);
            return View(application);
        }

        // POST: Applications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,ApplicationDateStart,ApplicationDateEnd,SubmittDate,Message,ReasonId,EmployeeId")] Application application)
        {
            if (id != application.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", application.EmployeeId);
            ViewData["ReasonId"] = new SelectList(_context.Reasons, "ReasonId", "ReasonId", application.ReasonId);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Employee)
                .Include(a => a.Reason)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
    }
}
