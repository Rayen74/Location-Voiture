namespace TP3Voiture;

public interface ILouable
{
   bool EstLoue{ get; }
   void Louer();
   void Retourner();
}