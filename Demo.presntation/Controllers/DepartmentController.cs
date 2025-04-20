using Microsoft.AspNetCore.Mvc;
using Demo.presentation.ViewModels;
using Demo.BusinessLogic.DataTransferObject;
using Demo.presntation.ViewModels.DepartmentViewModel;
using Demo.BusinessLogic.Services.Interfaces;


namespace Demo.presntation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment ):Controller
    {


        //BaseUrl/Departments/Index

        [HttpGet]
        public IActionResult Index()
        {
            var departments =_departmentService.GetAllDepartments(); 
            return View(departments);
        }

        #region  Create Department

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDepartmentDto departmentDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    int Result = _departmentService.AddDepartment(departmentDTO);
                    if (Result > 0)
                    {
                        return  RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't be created");
                        return View(departmentDTO);
                    }
                }
                catch(Exception ex)
                {
                    if(_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    else
                    {
                        _logger.LogError(ex.Message);

                    }

                }
            }
            return View(departmentDTO);

        }

        #endregion

        #region Department Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();  // state 400

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();  // state 404
            return View(department);

        }


        #endregion

        #region Department Edit

        [HttpGet]

        public IActionResult Edit(int? id )
        {
            if (!id.HasValue) return BadRequest();

            var department =_departmentService.GetDepartmentById(id.Value); 
            if (department is null) return NotFound();

            var departmentEditViewModel = new DepartmentEditeViewModel
            {
                Name = department.Name,
                Code = department.Code,
                Description=department.Description,
                DateOfCreation=department.DateOfCreation,   
            };
            return View(departmentEditViewModel);

        }

        public IActionResult Edit([FromRoute]int id,DepartmentEditeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdateDepartment = new UpdateDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        DateOfCreation = viewModel.DateOfCreation,
                        Description = viewModel.Description,
                        Name = viewModel.Name
                    };
                    int result = _departmentService.UpdateDepartment(UpdateDepartment);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not updated");
                        return View(viewModel);
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
            }

            return View(viewModel);
        }




        #endregion


        #region Department Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department == null) return NotFound();

            return View(department);

        }


        //the vedio of delete in 8 minuts 

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id==0) return BadRequest();

            try
            {
                bool Deleted = _departmentService.DelateDepartment(id);
                if (Deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not Deleted");
                    return RedirectToAction(nameof(Delete),new {id});
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

        #endregion

    }
}
