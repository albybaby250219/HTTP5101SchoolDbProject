using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5101SchoolDbProject.Models;

namespace HTTP5101SchoolDbProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET: Teacher/List
        /// <summary>
        /// The controller connects the view and the teacher data controller
        /// </summary>
        /// <returns>The view of the list f teachers</returns>
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }
        //GET: Teacher/Show/{TeacherId}
        /// <summary>
        /// The controller connects to the view of 1 teacher with the id selected
        /// </summary>
        /// <param name="id">Teacherid</param>
        /// <returns>Details of 1 Teacher</returns>
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

    }
}