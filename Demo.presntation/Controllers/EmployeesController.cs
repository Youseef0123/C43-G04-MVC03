using AutoMapper;
using Demo.BusinessLogic.DataTransferObject;
using Demo.BusinessLogic.DataTransferObject.EmployeeDTO;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.model;
using Demo.DataAccess.model.EmployeeModel;
using Demo.DataAccess.model.shared;
using Demo.presntation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;


namespace Demo.presntation.Controllers
{
    public class EmployeesController(IEmployeeService employeeService,
        ILogger<EmployeesController> _logger,
        IWebHostEnvironment _environment) : Controller
    {


        public IActionResult Index(string? EmployeeSearchName)
        {
            var empolyee = employeeService.GetAllEmployees(EmployeeSearchName);

            return View(empolyee);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }



        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto createdEmployeeDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //Mapping for EmployeeViewModel to CreatedEmployeeDTO     why??!!     to can make the partial view we must created model common 
                    //for the all parts (Edit , create ) so we gone make the EmployeeViewModel to be the model in partial view 
                    //and now we mapping it for CreatedEmployeeDTO to prevent the conflict that may be accoure in future 

                    //var createdEmployeeDto = new CreatedEmployeeDto()
                    //{
                    //    Name = employeeViewModel.Name,
                    //    Age = employeeViewModel.Age,
                    //    Address = employeeViewModel.Address,
                    //    Email = employeeViewModel.Email,
                    //    Gender = employeeViewModel.Gender,
                    //    IsActive = employeeViewModel.IsActive,
                    //    EmployeeType = employeeViewModel.EmployeeType,
                    //    HiringDate = employeeViewModel.HiringDate,
                    //    PhoneNumber= employeeViewModel.PhoneNumber,
                    //    Salary= employeeViewModel.Salary,

                    //    departmentId=employeeViewModel.departmentId 
                         
                    //};

                    var employee= employeeService.createEmployee(createdEmployeeDto);
                    if (employee > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Con't be created");
                        return View(createdEmployeeDto);

                    }


                }catch(Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    else
                    {
                        _logger.LogError(ex.Message);

                    }

                }
            }
            return View(createdEmployeeDto);
 
        }


        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue) return BadRequest();

            var employee = employeeService.GetEmployeeById(Id.Value);

            if (employee is null) return NotFound();
            return View(employee);
        }


        [HttpGet]
        public IActionResult Edite(int? Id)
        {
            if (!Id.HasValue) return BadRequest();

            var employee=employeeService.GetEmployeeById(Id.Value);
            if (employee is null) return NotFound();

            var updateDto = new UpdateEmploteeDTO()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Addrees,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HireDate,
                Gender = Enum.Parse<Gendr>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
            };

            return View(updateDto);   
        }


        [HttpPost]
        public IActionResult Edite( [FromRoute] int? id , UpdateEmploteeDTO updateEmploteeDTO)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(updateEmploteeDTO);

            try
            {
               //var updateEmploteeDTO = new UpdateEmploteeDTO()
               //{
               //    Id = id.Value,
               //    Name = employeeViewModel.Name,
               //    Salary = employeeViewModel.Salary,
               //    Address= employeeViewModel.Address,
               //    Age = employeeViewModel.Age,
               //    Email = employeeViewModel.Email,
               //    PhoneNumber = employeeViewModel.PhoneNumber,
               //    IsActive = employeeViewModel.IsActive,
               //    EmployeeType = employeeViewModel.EmployeeType,
               //    Gender= employeeViewModel.Gender,
               //    HiringDate  =   employeeViewModel.HiringDate,

               //    departmentId=employeeViewModel.departmentId,

               //};


                var employeeEdit = employeeService.updateEmployee(updateEmploteeDTO);

                if (employeeEdit > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "the employee not updated");
                    return View(updateEmploteeDTO);
                }

            }
            catch(Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(updateEmploteeDTO);

                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("Error view ", ex);

                }
            }


        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var RemoveEmployee = employeeService.GetEmployeeById(id.Value);
            if (RemoveEmployee is null) return NotFound();
            return View(RemoveEmployee);



        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            try
            {

                var DeleteEmployee = employeeService.deleteEmployee(id);

                if (DeleteEmployee)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The Employee not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("Error view ", ex);

                }
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
