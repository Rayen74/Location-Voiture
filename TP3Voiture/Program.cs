using System;
using System.Windows.Forms;

namespace TP3Voiture;

static class Program
{
    /// <summary>
    ///  Point d'entrée principal de l'application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Initialise les configurations graphiques (DPI, styles visuels)
        ApplicationConfiguration.Initialize();

        // LANCE VOTRE FORMULAIRE PRINCIPAL (FormAgence)
        Application.Run(new FormAgence());
    }
}