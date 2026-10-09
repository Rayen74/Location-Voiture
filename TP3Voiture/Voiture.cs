namespace TP3Voiture;
public class Voiture : Vehicule
{
    public int NombrePortes { get; set; }

    public Voiture(string marque, string modele, int annee, double prix, int nombrePortes)
        : base(marque, modele, annee, prix)
    {
        NombrePortes = nombrePortes;
    }

    public override string AfficherInfos() =>
        $"[Voiture] ID:{Id} - {Marque} {Modele} ({Annee}) - {PrixLocationJour}€/j - Portes: {NombrePortes} - Loué: {EstLoue}";
}