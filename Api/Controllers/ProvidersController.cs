using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    using Application;

    using log4net;

    [RoutePrefix("api")]
    public class ProvidersController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ProvidersController));

        private readonly IOrderManagementService orderManagementService;

        public ProvidersController(IOrderManagementService orderManagementService)
        {
            this.orderManagementService = orderManagementService;
        }

        [Route("providers")]
        [HttpGet]
        public IHttpActionResult GetProviders()
        {
            log.Info("Query has been made...");
            try
            {
                return this.Ok(this.orderManagementService.GetProviders());
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        [Route("providers")]
        [HttpPost]
        public IHttpActionResult AddProviders()
        {
            try
            {
                this.orderManagementService.AddProviders();
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }
    }
}
