﻿using System.Diagnostics.CodeAnalysis;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController()
        {
            _service = new AuthService();
        }
        
        /// <summary>
        /// Obter todos os usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        [Authorize]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
            => Ok(_service.GetAll());

        /// <summary>
        /// Registrar novo usuário
        /// </summary>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public ActionResult<IEnumerable<UserDTO>> SignUp(UserDTO dto)
            => Ok(_service.SignUp(dto));

        /// <summary>
        /// Realizar login com nome de usuário e senha
        /// </summary>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public ActionResult<IEnumerable<UserDTO>> Authenticate(UserDTO dto)
            => Ok(_service.Authenticate(dto));

        /// <summary>
        /// Deletar um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
            => Ok(_service.Delete(id));
    }
}
