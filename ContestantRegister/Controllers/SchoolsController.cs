﻿using System;
using System.Linq;
using ContestantRegister.Data;
using ContestantRegister.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Utils;
using Microsoft.AspNetCore.Authorization;
using ContestantRegister.Utils.Filter;
using ContestantRegister.Utils.Ddd;

namespace ContestantRegister.Controllers
{
    public class SchoolFitler
    {
        [StringFilter(StringFilter.Contains, IgnoreCase = true)]
        public string ShortName { get; set; }
        [StringFilter(StringFilter.Contains, IgnoreCase = true)]
        [RelatedObject("City", PropertyName = "Name")]
        public string City { get; set; }
    }

    [Authorize(Roles = Roles.Admin)]
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public SchoolsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Schools
        public async Task<IActionResult> Index(SchoolFitler filter)
        {
            ViewData["shortNameFilter"] = filter.ShortName;
            ViewData["cityFilter"] = filter.City;

            // Sample of specs for navigation properties
            //if (!string.IsNullOrEmpty(filter.City))
            //{
            //    var citySpec = new Spec<City>(x => x.Name.ToLower().Contains(filter.City.ToLower()));
            //    var testSchools = _context.Schools
            //        .Include(s => s.City)
            //        .Where(x => x.City, citySpec)
            //        .OrderBy(item => item.ShortName)
            //        .ToList();

            //}

            IQueryable<School> schools = _context.Schools
                .Include(s => s.City)
                .AutoFilter(filter)
                .OrderBy(item => item.ShortName);            

            return View(await schools.ToListAsync());
        }

        // GET: Schools/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(c => c.Name), "Id", "Name");
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,CityId,ShortName,FullName,Site,Id")] School school)
        {
            if (ModelState.IsValid)
            {
                _context.Add(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(c => c.Name), "Id", "Name", school.CityId);
            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools.SingleOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(c => c.Name), "Id", "Name", school.CityId);
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,CityId,ShortName,FullName,Site,Id")] School school)
        {
            if (id != school.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbSchool = await _context.Schools.SingleOrDefaultAsync(x => x.Id == id);
                if (dbSchool == null)
                {
                    return NotFound();
                }

                _mapper.Map(school, dbSchool);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(c => c.Name), "Id", "Name", school.CityId);
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.City)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var school = await _context.Schools.SingleOrDefaultAsync(m => m.Id == id);
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
