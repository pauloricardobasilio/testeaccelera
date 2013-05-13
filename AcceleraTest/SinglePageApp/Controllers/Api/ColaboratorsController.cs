using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Application;
using Application.Base;
using Domain;

namespace SinglePageApp.Controllers.Api
{
    public class ColaboratorsController : ApiController
    {
        public IEnumerable<Colaborator> Get()
        {
            var service = ColaboratorServiceFactory.Create();
            var colaborators = service.FindColaborators();
            return colaborators.ToList().OrderByDescending(x => x.Id);
        }

        public IEnumerable<Colaborator> Get(string filter)
        {
            var service = ColaboratorServiceFactory.Create();

            if (string.IsNullOrEmpty(filter))
                return service.FindColaborators();
            return service.FindColaborators(x => x.Name.Contains(filter) || x.Registry == filter);
        }

        public HttpResponseMessage Get(int id)
        {
            var service = ColaboratorServiceFactory.Create();
            var colaborator = service.FindColaboratorById(id);

            if (colaborator == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError("Este colaborador não existe"));

            return Request.CreateResponse(HttpStatusCode.OK, colaborator);
        }

        public HttpResponseMessage Put(Colaborator colaborator)
        {
            if (colaborator == null || colaborator.Id == 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Este colaborador não existe");

            var service = ColaboratorServiceFactory.Create();

            try
            {
                service.UpdateColaborator(colaborator);
            }
            catch (ApplicationValidationErrorsException exception)
            {
                var errors = new ModelStateDictionary();
                foreach (var validationError in exception.ValidationErrors)
                    errors.AddModelError(validationError.Key, validationError.Value);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors);
            }


            return Request.CreateResponse(HttpStatusCode.OK, colaborator);
        }

        public HttpResponseMessage Post(Colaborator colaborator)
        {
            if (colaborator == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Colaborador Inválido");

            var service = ColaboratorServiceFactory.Create();

            try
            {
                service.AddColaborator(colaborator);
            }
            catch (ApplicationValidationErrorsException exception)
            {
                var errors = new ModelStateDictionary();
                foreach (var error in exception.ValidationErrors)
                    errors.AddModelError(error.Key, error.Value);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors);
            }

            return Request.CreateResponse(HttpStatusCode.OK, colaborator);
        }

        public void Delete(int id)
        {
            var service = ColaboratorServiceFactory.Create();
            service.RemoveColaborator(id);
        }
    }
}
