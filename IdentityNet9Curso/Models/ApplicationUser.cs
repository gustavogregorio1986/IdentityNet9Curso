using Microsoft.AspNetCore.Identity;

namespace IdentityNet9Curso.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}
