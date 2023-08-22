using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource your requested couldn`t be found!";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    _logger.LogWarning($"404 error occured. Path {statusCodeResult.OriginalPath} and QueryString = {statusCodeResult.OriginalQueryString}");
                    return View("NotFound");
                //break;
                case 403:
                    ViewBag.ErrorMessage = "Sorry, access denied!";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    _logger.LogWarning($"403 error occured. Path {statusCodeResult.OriginalPath} and QueryString = {statusCodeResult.OriginalQueryString}");
                    return RedirectToAction("Index", "AccessDenied");
                //break;
                case 401:
                    ViewBag.ErrorMessage = "Sorry, access denied!";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    //ViewBag.ReturnURL
                    _logger.LogWarning($"403 error occured. Path {statusCodeResult.OriginalPath} and QueryString = {statusCodeResult.OriginalQueryString}");
                    return RedirectToAction("Index", "Login", new { returnUrl = "Test"});
                //break;
                default:
                    return View("NotFound");
            }
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            _logger.LogInformation($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");
            return View("Error");
        }
    }
}
