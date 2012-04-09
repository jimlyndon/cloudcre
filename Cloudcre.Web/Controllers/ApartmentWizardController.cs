using System;
using System.Web.Mvc;
using Cloudcre.Infrastructure.CookieStorage;
using Cloudcre.Service.Property;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Service.Property.ViewModels;
using Microsoft.Web.Mvc;

namespace Cloudcre.Web.Controllers
{
    public class ApartmentWizardController : WizardController<MultipleFamilyViewModel>
    {
        public class JsonRedirectToAction
        {
            public string route { get; set; }
        }

        private readonly MultipleFamilyService _multipleFamilyService;

        private readonly MultipleFamilyViewModel _emptyDefaultTemplate = new MultipleFamilyViewModel();

        public ApartmentWizardController(ICookieStorageService cookieStorageService, MultipleFamilyService multipleFamilyService)
            : base(cookieStorageService)
        {
            if (multipleFamilyService == null)
                throw new ArgumentException("PropertySaleService");

            _multipleFamilyService = multipleFamilyService;
        }

        [HttpPost]
        public ActionResult StepOne(string cancelButton, string nextButton, Guid? id)
        {
            if (cancelButton != null)
                return Cancel();

            if (!ModelState.IsValid)
                return Json(new JsonRedirectToAction { route = "StepOne" });

            if (string.IsNullOrEmpty(nextButton) && id.HasValue)
            {
                GetPropertyResponse<MultipleFamilyViewModel> response = _multipleFamilyService.GetProperty(new GetPropertyRequest<Guid> { Id = id.Value });

                viewModel = response.ViewModel;
            }

            SerializeViewModelBetweenActionCalls();

            if (nextButton != null)
                return Json(new JsonRedirectToAction { route = "StepTwo" });

            // initial page load
            return Json(viewModel);
        }

        [HttpPost]
        public ActionResult StepTwo(string cancelButton, string backButton, string nextButton)
        {
            if (cancelButton != null)
                return Cancel();

            // back button selected, show previous screen
            if (backButton != null)
                return Json(new JsonRedirectToAction { route = "StepOne" });

            if (!ModelState.IsValid)
                return Json(new JsonRedirectToAction { route = "StepTwo" });

            SerializeViewModelBetweenActionCalls();

            // validation successful, wizard moving to next screen
            if (nextButton != null && ModelState.IsValid)
                return Json(new JsonRedirectToAction { route = "StepThree" });

            return Json(viewModel);
        }

        [HttpPost]
        public ActionResult StepThree(string cancelButton, string backButton, string nextButton)
        {
            if (cancelButton != null)
                return Cancel();

            // back button selected, show previous screen
            if (backButton != null)
                return Json(new JsonRedirectToAction { route = "StepTwo" });

            if (!ModelState.IsValid)
                return Json(new JsonRedirectToAction { route = "StepThree" });
            //Response.StatusCode = (int)HttpStatusCode.BadRequest;

            SerializeViewModelBetweenActionCalls();

            // validation successful, wizard moving to next screen
            if (nextButton != null)
                return Json(new JsonRedirectToAction { route = "Complete" });

            // inital load of partial or validation fail, in wihich case we show the partial again with validation errors
            return Json(viewModel);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            if (ModelState.IsValid)
            {
                var request = new AddPropertyRequest<MultipleFamilyViewModel>();
                request.ViewModel = viewModel;
                AddPropertyResponse response = _multipleFamilyService.AddProperty(request);
                //response.Success //message 
            }
            // TODO relace empty json object with json success msg
            return Json(new { });
        }

        [HttpGet]
        public ActionResult StepOne()
        {
            return View(_emptyDefaultTemplate);
        }

        [HttpGet]
        public ActionResult StepTwo()
        {
            return View(_emptyDefaultTemplate);
        }

        [HttpGet]
        public ActionResult StepThree()
        {
            return View(_emptyDefaultTemplate);
        }

        private ActionResult Cancel()
        {
            viewModel = new MultipleFamilyViewModel();
            SerializeViewModelBetweenActionCalls();
            return Json(viewModel);
        }


        private void SerializeViewModelBetweenActionCalls()
        {
            viewModel.serializedViewModel = new MvcSerializer().Serialize(viewModel, SerializationMode.EncryptedAndSigned);
        }
    }
}