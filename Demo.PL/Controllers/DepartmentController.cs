using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {

            var departments = await _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
               await _departmentRepository.Add(department);

                return RedirectToAction(nameof(Index));
            }
            return View(department);

        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id , string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

           var department = await _departmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();

            return View(viewName,department);
             
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await  Details(id , "Edit");
        }

        [HttpPost]
        public IActionResult Edit( [FromRoute]int id , Department department)
        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Uodate(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);


                }

            }

            return View(department);
        }


        [HttpGet]
        public async Task<IActionResult> Delete( int id)
        {
            return await  Details(id, "Delete");
        }


        [HttpPost]
        public IActionResult Delete([FromRoute]int id ,  Department department )
        {
            if (id != department.Id)
                return null;

            _departmentRepository.Delete(department);
          
            return RedirectToAction(nameof(Index));
        }
    }
}
