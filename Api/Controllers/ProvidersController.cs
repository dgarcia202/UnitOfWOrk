using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    using Application;

    [RoutePrefix("api")]
    public class ProvidersController : ApiController
    {
        private readonly IOrderManagementService orderManagementService;

        public ProvidersController(IOrderManagementService orderManagementService)
        {
            this.orderManagementService = orderManagementService;
        }

        [Route("providers")]
        [HttpGet]
        public IHttpActionResult GetProviders()
        {
            try
            {
                return this.Ok(this.orderManagementService.GetProviders());
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        } 
    }
}
