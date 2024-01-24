using System.ComponentModel.DataAnnotations;

namespace PeopleApp.Models;

public class Persona
{
    [Key]
    public int PersonaId { get; set; }
    [Required (ErrorMessage = "El campo Nombre es requerido")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo Telefono es requerido")]
    public string Telefono { get; set; } = string.Empty;
}