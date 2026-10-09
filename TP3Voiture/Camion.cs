namespace TP3Voiture;

public class Camion : Vehicule
{
    private double _capaciteChargement;
    public double CapaciteChargement
    {
        get => _capaciteChargement;
        set
        {
            if (value < 0)
                throw new ArgumentException("La capacité de chargement ne peut pas être négative.");
            _capaciteChargement = value;
        }
    }

    public Camion(string marque, string modele, int annee, double prix, double capaciteChargement)
        : base(marque, modele, annee, prix)
    {
        CapaciteChargement = capaciteChargement;
    }

    public override string AfficherInfos() =>
        $"[Camion] ID:{Id} - {Marque} {Modele} ({Annee}) - {PrixLocationJour}€/j - Capacité: {CapaciteChargement}T - Loué: {EstLoue}";
}