using System.ComponentModel.DataAnnotations;

namespace ClinicaMVC.Models
{
    public class Paciente
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "La edad debe ser mayor a 0")]
        public int Edad { get; set; }

        public bool TienePrevision { get; set; }

        [Range(1, 3, ErrorMessage = "Debe seleccionar un tipo de atención válido")]
        public int TipoAtencion { get; set; }

        public int CostoBase { get; set; }

        public int Descuento { get; set; }

        public int TotalPagar { get; set; }
    }
}
