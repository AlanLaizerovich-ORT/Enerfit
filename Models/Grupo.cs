namespace Enerfit.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioCreadorId { get; set; }

        public bool SoyMiembro { get; set; }
        public int CantidadMiembros { get; set; }
    }
}
