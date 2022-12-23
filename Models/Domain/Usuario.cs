namespace AppWeb.Models.Domain
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salario { get; set; }
        public int Edad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
