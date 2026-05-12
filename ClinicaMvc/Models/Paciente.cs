using System.ComponentModel.DataAnnotations;

namespace ClinicaMVC.Models
{
    public class Paciente
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty; // Los Getter y Setter de Nombre que es un String

        [Range(1, 120, ErrorMessage = "La edad debe ser mayor a 0")] // Nos permite ingresar una edad entre 1 y 120
        public int Edad { get; set; } //  Getter y Setter de Edad, es del tipo INT

        public bool TienePrevision { get; set; } // Un dato Booleano con Getter y Settter

        [Range(1, 3, ErrorMessage = "Debe seleccionar un tipo de atención válido")] // Nos Permite escoger entre 3 Opciones
        public int TipoAtencion { get; set; } // Getter y Setter de Tipo de Atencion es del tipo INT

        public int CostoBase { get; set; } // Getter y Setter de Costo Base, es del tipo INT

        public int Descuento { get; set; } // Getter y Setter de Descuento, es del tipo INT

        public int TotalPagar { get; set; } // // Getter y Setter de Total a pagar, es del tipo INT
    }
}
