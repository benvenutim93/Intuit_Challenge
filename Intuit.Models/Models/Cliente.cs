using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Intuit.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Cuit { get; set; } = null!;

    public string Domicilio { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj != null && typeof(Cliente) == obj.GetType())
        {
            return this.Nombres == ((Cliente)obj).Nombres &&
                this.Apellidos == ((Cliente)obj).Apellidos &&
                this.FechaNacimiento == ((Cliente)obj).FechaNacimiento &&
                this.Cuit == ((Cliente)obj).Cuit &&
                this.Domicilio == ((Cliente)obj).Domicilio &&
                this.Telefono == ((Cliente)obj).Telefono &&
                this.Email == ((Cliente)obj).Email;
        }

        return false;
    }
}
