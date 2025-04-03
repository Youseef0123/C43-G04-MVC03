using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Demo.presentation.ViewModels;


namespace Demo.presntation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService ):Controller
    {
      
        public IActionResult Index()
        {
            var departments =departmentService.GetAllDepartments(); 
            return View();
        }

      

    }
}
