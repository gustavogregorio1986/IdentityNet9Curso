using IdentityNet9Curso.Dtos;
using IdentityNet9Curso.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNet9Curso.Services.Auth
{
    public class AuthService : IAuthInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseModel<string>> Register(RegistroDto registroDto)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            try
            {
                // 1. Validar se as Roles enviadas existem antes de criar o usuário (com proteção contra null)
                if (registroDto.Roles != null && registroDto.Roles.Any())
                {
                    foreach (var role in registroDto.Roles)
                    {
                        if (!await _roleManager.RoleExistsAsync(role))
                        {
                            response.Mensagem = $"A role '{role}' não existe no sistema.";
                            response.Status = false;
                            return response;
                        }
                    }
                }

                // 2. Criar o usuário
                var user = new ApplicationUser
                {
                    Email = registroDto.Email,
                    NomeCompleto = registroDto.NomeCompleto,
                    UserName = registroDto.Usuario
                };

                var result = await _userManager.CreateAsync(user, registroDto.Senha);

                if (!result.Succeeded)
                {
                    var erros = string.Join(", ", result.Errors.Select(e => e.Description));
                    response.Mensagem = $"Erro ao registrar usuário: {erros}";
                    response.Status = false;
                    return response;
                }

                // 3. Adicionar as Roles ao usuário recém-criado
                if (registroDto.Roles != null && registroDto.Roles.Any())
                {
                    var roleResult = await _userManager.AddToRolesAsync(user, registroDto.Roles);

                    if (!roleResult.Succeeded)
                    {
                        response.Mensagem = "Usuário criado, mas houve erro ao atribuir as roles.";
                        response.Status = false;
                        return response;
                    }
                }

                response.Mensagem = "Usuário registrado com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}