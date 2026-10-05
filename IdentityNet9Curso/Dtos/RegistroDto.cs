namespace IdentityNet9Curso.Dtos
{
    public class RegistroDto
    {
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
