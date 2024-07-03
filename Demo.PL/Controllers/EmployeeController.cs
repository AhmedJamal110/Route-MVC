using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
     
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public EmployeeController( IUintOfWork uintOfWork, IMapper mapper )
        {
            
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
            
                employees = await _uintOfWork.EmployeeRepository.GetAll();
       
            else
               employees =  _uintOfWork.EmployeeRepository.GetEmployessByName(SearchValue);
               
            
           
             var MappedEmpToEmpView = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(MappedEmpToEmpView);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.departments = _departmentRepository.GetAll();
            
            return View();

        }

        [HttpPost]
        public async Task<IActionResult>  Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {

                employee.ImageName = DocumentSetting.UploadFile( employee.Image , "images"); ;
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);
                await _uintOfWork.EmployeeRepository.Add(MappedEmployee);

                await _uintOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);

        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = await  _uintOfWork.EmployeeRepository.GetById(id.Value);
            if (department is null)
                return NotFound();

            return View(viewName, department);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _uintOfWork.EmployeeRepository.Uodate(employee);

                   await  _uintOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);


                }

            }

            return View(employee);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return null;

            _uintOfWork.EmployeeRepository.Delete(employee);
           var result = await  _uintOfWork.Complete();

            if(result > 0)
            {
                DocumentSetting.DelelteFile(employee.ImageName, "images");
            }


            return RedirectToAction(nameof(Index));
        }
    }

}

