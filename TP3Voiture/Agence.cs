using System;
using System.ComponentModel;
using System.Linq;

namespace TP3Voiture;

public class Agence
{
    // BindingList permet la mise à jour automatique dans le DataGridView
    public BindingList<Vehicule> Vehicules { get; } = new BindingList<Vehicule>();

    public void AjouterVehicule(Vehicule v)
    {
        if (v == null) throw new ArgumentNullException(nameof(v));
        Vehicules.Add(v);
    }

    public BindingList<Vehicule> ListerVehicules() => Vehicules;

    public BindingList<Vehicule> RechercherParMarque(string marque)
    {
        var resultats = Vehicules
            .Where(v => v.Marque.Equals(marque, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return new BindingList<Vehicule>(resultats);
    }

    public BindingList<Vehicule> RechercherDisponibles()
    {
        var resultats = Vehicules.Where(v => !v.EstLoue).ToList();
        return new BindingList<Vehicule>(resultats);
    }

    public BindingList<Vehicule> FiltrerParType(string type)
    {
        var resultats = type.ToLower() switch
        {
            "voiture" => Vehicules.OfType<Voiture>().Cast<Vehicule>().ToList(),
            "moto" => Vehicules.OfType<Moto>().Cast<Vehicule>().ToList(),
            "camion" => Vehicules.OfType<Camion>().Cast<Vehicule>().ToList(),
            _ => Vehicules.ToList()
        };

        return new BindingList<Vehicule>(resultats);
    }
}