using Demo.BusinessLogic.DataTransferObject.EmployeeDTO;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.model.EmployeeModel;
using Demo.DataAccess.model.shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;


namespace Demo.presntation.Controllers
{
    public class EmployeesController(IEmployeeService employeeService,
        ILogger<EmployeesController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var empolyee = employeeService.GetAllEmployees();

            return View(empolyee);
        }


        [HttpGet]
        public IActionResult Create() => View();


        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto createdEmployeeDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
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

            var employeeDTO = new UpdateEmploteeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Addrees,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate =employee.HireDate,
                Gender  =Enum.Parse<Gendr>(employee.Gender),
                EmployeeType=Enum.Parse<EmployeeType>(employee.EmployeeType)

            };

            return View(employeeDTO);   
        }


        
        public IActionResult Edite( [FromRoute] int? id , UpdateEmploteeDTO updateEmploteeDTO)
        {
            if (!id.HasValue || id != updateEmploteeDTO.Id) return BadRequest();
            if (!ModelState.IsValid) return View(updateEmploteeDTO);

            try
            {
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
