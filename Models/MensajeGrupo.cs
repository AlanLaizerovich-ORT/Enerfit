namespace Enerfit.Models
{
    public class MensajeGrupo
    {
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public int UsuarioId { get; set; }

        public string UsuarioNombre { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
