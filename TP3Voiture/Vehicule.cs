namespace TP3Voiture;

public abstract class Vehicule : ILouable
{
   private static int _compteur = 0;
   
   public int Id { get; }
   public string Nom { get; set; }
   public string Marque { get; set; }
   public int Annee { get; set; }
   public string Modele  { get; set; }
   private double _PrixLocationJour;
   public double PrixLocationJour{
       get => _PrixLocationJour;
       set
       {
           if (value < 0 )
               throw new ArgumentException("le prix de location ne doit pas etre négatif");
       }
   }

   public bool EstLoue { get; private set; } =  false;

   protected Vehicule(string marque, string modele, int annee, double prixLocationJour)
   {
       Id = _compteur++;
       Marque = marque;
       Modele = modele;
       PrixLocationJour = prixLocationJour;
       Annee = annee;
   }

   public void Louer()
   {
        if(EstLoue)
            throw new InvalidOperationException($"Le vehicule ID {Id} {Marque} {Modele} est déja loué");
        EstLoue = true;
   }
   
   public void Retourner()
   {
       if (!EstLoue)
           throw new InvalidOperationException($"le vehicule ID{Id} {Marque} {Modele} est actuellement non loué");
       EstLoue = false;
   }

   public abstract string AfficherInfos();
}