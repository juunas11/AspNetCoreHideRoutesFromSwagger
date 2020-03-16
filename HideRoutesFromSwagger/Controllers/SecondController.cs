using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace HideRoutesFromSwagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecondController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { Something = "A" });
        }
    }

    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            // Replace with any logic you want
            if (action.Controller.ControllerName == "Second")
            {
                action.ApiExplorer.IsVisible = false;
            }
        }
    }

    public class ControllerHidingConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // Replace with any logic you want
            if (controller.ControllerName == "Second")
            {
                // This does not work
                //controller.ApiExplorer.IsVisible = false;

                // We need to hide all the actions
                foreach (ActionModel action in controller.Actions)
                {
                    action.ApiExplorer.IsVisible = false;
                }
            }
        }
    }

    public class ControllerHidingAppConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            // We can also do it with an application model convention
            // Replace with any logic you want
            foreach (ControllerModel controller in application.Controllers.Where(c => c.ControllerName == "Second"))
            {
                // This does not work
                //controller.ApiExplorer.IsVisible = false;

                // We need to hide all the actions
                foreach (ActionModel action in controller.Actions)
                {
                    action.ApiExplorer.IsVisible = false;
                }
            }
        }
    }
}
