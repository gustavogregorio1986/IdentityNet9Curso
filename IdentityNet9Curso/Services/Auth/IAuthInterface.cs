using IdentityNet9Curso.Dtos;
using IdentityNet9Curso.Models;

namespace IdentityNet9Curso.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ResponseModel<string>> Register(RegistroDto registroDto);
    }
}
