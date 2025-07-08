using System;
using Microsoft.AspNetCore.Mvc;
using marcatel_api.Services;
using marcatel_api.Utilities;
using Microsoft.AspNetCore.Authorization;
using marcatel_api.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using marcatel_api.Helpers;

namespace marcatel_api.Controllers
{
   
    [Route("api")]
    public class AdminController: ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly ILogger<AdminController> _logger;
  
        private readonly IJwtAuthenticationService _authService;


        Encrypt enc = new Encrypt();

        public AdminController(AdminService adminservice, ILogger<AdminController> logger, IJwtAuthenticationService authService) {
            _adminService = adminservice;
            _logger = logger;
       
            _authService = authService;
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("GetCategorias")]
        public JsonResult GetCategorias()
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _adminService.GetCategorias(1);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data = articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("GetDimVentasArticulo")]
        public JsonResult GetDimVentasArticulo(int sucursal, string f_inicial, string f_final)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _adminService.GetDimVentasArticulo(sucursal, f_inicial, f_final);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data = articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("GetVentasFamiliaSucursal")]
        public JsonResult GetVentasFamiliaSucursal(int sucursal, string f_inicial, string f_final)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _adminService.GetVentasFamiliaSucursal( f_inicial, f_final, sucursal);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data = articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("GetVentasProveedores")]
        public JsonResult GetVentasProveedor(int sucursal, string f_inicial, string f_final)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _adminService.GetVentasProveedor( f_inicial, f_final, sucursal);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data = articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("GetVentasDeptoSucursal")]
        public JsonResult GetVentasDepartamentoSucursal(int sucursal, string f_inicial, string f_final)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _adminService.GetVentasDepartamentoSucursal( f_inicial, f_final, sucursal);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data = articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

        [HttpGet("GetTotalesSucursal")]
        public JsonResult GetTotalesSucursal( string f_inicial, string f_final)
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                var articulo = _adminService.GetTotalVentasCantidad( f_inicial, f_final);
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                objectResponse.response = new
                {
                    data = articulo
                };
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return new JsonResult(objectResponse);

        }

    }
}
