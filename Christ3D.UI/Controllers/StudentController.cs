using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentAppService _studentAppService;
        private readonly DomainNotificationHandler _notifications;

        public StudentController(IStudentAppService studentAppService, IMemoryCache cache, INotificationHandler<DomainNotification> notifications)
        {
            _studentAppService = studentAppService;
            // 强类型转换
            _notifications = (DomainNotificationHandler)notifications;
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_studentAppService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            try
            {

                if (!ModelState.IsValid)
                    return View(studentViewModel);

                _studentAppService.Register(studentViewModel);

                if (!_notifications.HasNotifications())
                    ViewBag.Sucesso = "Student Registered!";

                return Redirect("Index");
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }
    }
}
