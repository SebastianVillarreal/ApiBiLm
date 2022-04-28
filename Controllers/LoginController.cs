using System;
using Microsoft.AspNetCore.Mvc;
using marcatel_api.Services;
using marcatel_api.Utilities;
using Microsoft.AspNetCore.Authorization;
using marcatel_api.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace marcatel_api.Controllers
{
   
    [Route("api")]
    public class LoginController: ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly ILogger<LoginController> _logger;
  
        private readonly IJwtAuthenticationService _authService;


        Encrypt enc = new Encrypt();

        public LoginController(LoginService loginservice, ILogger<LoginController> logger, IJwtAuthenticationService authService) {
            _loginService = loginservice;
            _logger = logger;
       
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public JsonResult SignIn([FromBody] AuthInfo user)
        {
            ResponseLogin result = new ResponseLogin();
            result.Response = new ResponseBody();
            result.Response.data = new DataResponseLogin();
            result.Response.data.Usuario = new UsuarioModel();
          
                string cryptedPass = enc.GetSHA256(user.Userpassword);
           
            var loginResponse = _loginService.Login(user.Username, user.Userpassword);

         
           
                if (loginResponse.Id != 0)
                {
                    result.StatusCode = (int)HttpStatusCode.OK;
                    result.Error = false;
                    result.Success = true;
                    result.Message = "Bienvenido";
                    result.Response.data.Usuario = loginResponse;
                    result.Response.data.Status = true;
                    result.Response.data.Mensaje = "Bienvenido";
                    var token = _authService.Authenticate(user.Username, cryptedPass);
                    result.Response.data.Token = token;
                }
                else
                {
                    result.Error = true;
                    result.Success = false;
                    result.Message = "Usuario o contraseña incorrecto,";

                }
            
            return new JsonResult(result);

        }
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("sandbox")]
        public JsonResult Sandbox()
        {
            return new JsonResult(new { Status = "Test Mac Hector" });
        }
    }
}