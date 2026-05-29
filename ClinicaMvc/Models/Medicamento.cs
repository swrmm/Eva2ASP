using System.ComponentModel.DataAnnotations;

namespace ClinicaMVC.Models;

public class Medicamento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(120, ErrorMessage = "El nombre no puede superar los 120 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripcion es obligatoria")]
    [StringLength(500, ErrorMessage = "La descripcion no puede superar los 500 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(1, 9999999, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Range(1, 99999, ErrorMessage = "El stock debe ser mayor a 0")]
    public int Stock { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de vencimiento")]
    [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
    public DateTime FechaVencimiento { get; set; } = DateTime.Today.AddMonths(6);

    [Required(ErrorMessage = "El laboratorio es obligatorio")]
    [StringLength(120, ErrorMessage = "El laboratorio no puede superar los 120 caracteres")]
    public string Laboratorio { get; set; } = string.Empty;

    [Required(ErrorMessage = "La categoria es obligatoria")]
    [StringLength(80, ErrorMessage = "La categoria no puede superar los 80 caracteres")]
    public string Categoria { get; set; } = string.Empty;
}
