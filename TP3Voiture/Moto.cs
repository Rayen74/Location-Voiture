namespace TP3Voiture;

public class Moto : Vehicule
{
    public int Cylindree { get; set; }

    public Moto(string marque, string modele, int annee, double prix, int cylindree)
        : base(marque, modele, annee, prix)
    {
        Cylindree = cylindree;
    }

    public override string AfficherInfos() =>
        $"[Moto] ID:{Id} - {Marque} {Modele} ({Annee}) - {PrixLocationJour}€/j - Cylindrée: {Cylindree}cc - Loué: {EstLoue}";
}