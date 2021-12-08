﻿using AppData.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            if (securityService == null)
            {
                throw new InvalidDataException("");
            }
            _securityService = securityService;
        }

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public ActionResult<TokenDto>Login(LoginDto dto)
        {
            try
            {
                var token = _securityService.GenerateToken(dto.UserName, dto.Password);
                return Ok(new TokenDto
                {
                    jwt = token.Jwt,
                    Mesage = token.Message
                });
            }
            catch(AuthenticationException ae)
            {
                return Unauthorized(ae.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"Please Contact Admin");
            }
        }
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class TokenDto
    {
        public string jwt { get; set; }
        public string Mesage { get; set; }
    }
}
