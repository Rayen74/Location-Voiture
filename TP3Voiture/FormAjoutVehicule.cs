using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP3Voiture;

public class FormAjoutVehicule : Form
{
    public Vehicule? VehiculeCree { get; private set; }

    private ComboBox cbType = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat };
    private TextBox txtMarque = new TextBox { Dock = DockStyle.Fill };
    private TextBox txtModele = new TextBox { Dock = DockStyle.Fill };
    private NumericUpDown numAnnee = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 1900, Maximum = 2100, Value = 2024 };
    private NumericUpDown numPrix = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Maximum = 10000 };
    private NumericUpDown numSpecifique = new NumericUpDown { Dock = DockStyle.Fill, Maximum = 10000 };
    
    private Label lblType = new Label { Text = "Type de véhicule :", AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) };
    private Label lblMarque = new Label { Text = "Marque :", AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) };
    private Label lblModele = new Label { Text = "Modèle :", AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) };
    private Label lblAnnee = new Label { Text = "Année :", AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) };
    private Label lblPrix = new Label { Text = "Prix / Jour (€) :", AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) };
    private Label lblSpecifique = new Label { Text = "Nombre de portes :", AutoSize = true, Anchor = AnchorStyles.Left, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) };

    private Button btnValider = new Button
    {
        Text = "✔ Valider et Ajouter",
        Dock = DockStyle.Fill,
        Height = 45,
        BackColor = Color.FromArgb(40, 167, 69),
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        Cursor = Cursors.Hand
    };

    public FormAjoutVehicule()
    {
        Text = "Ajouter un nouveau Véhicule";
        Size = new Size(520, 500);
        MinimumSize = new Size(480, 480);
        StartPosition = FormStartPosition.CenterParent;
        Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
        BackColor = Color.FromArgb(245, 247, 250);

        btnValider.FlatAppearance.BorderSize = 0;

        cbType.Items.AddRange(new object[] { "Voiture", "Moto", "Camion" });
        cbType.SelectedIndex = 0;
        cbType.SelectedIndexChanged += CbType_SelectedIndexChanged;

        TableLayoutPanel layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 8,
            Padding = new Padding(25),
            BackColor = Color.White
        };

        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

        for (int i = 0; i < 6; i++)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        }
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        layout.Controls.Add(lblType, 0, 0);
        layout.Controls.Add(cbType, 1, 0);

        layout.Controls.Add(lblMarque, 0, 1);
        layout.Controls.Add(txtMarque, 1, 1);

        layout.Controls.Add(lblModele, 0, 2);
        layout.Controls.Add(txtModele, 1, 2);

        layout.Controls.Add(lblAnnee, 0, 3);
        layout.Controls.Add(numAnnee, 1, 3);

        layout.Controls.Add(lblPrix, 0, 4);
        layout.Controls.Add(numPrix, 1, 4);

        layout.Controls.Add(lblSpecifique, 0, 5);
        layout.Controls.Add(numSpecifique, 1, 5);

        layout.Controls.Add(btnValider, 0, 7);
        layout.SetColumnSpan(btnValider, 2);

        Controls.Add(layout);
        btnValider.Click += BtnValider_Click;
    }

    private void CbType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        lblSpecifique.Text = cbType.SelectedItem?.ToString() switch
        {
            "Voiture" => "Nombre de portes :",
            "Moto" => "Cylindrée (cc) :",
            "Camion" => "Capacité (Tonnes) :",
            _ => "Spécifique :"
        };
    }

    private void BtnValider_Click(object? sender, EventArgs e)
    {
        try
        {
            string marque = txtMarque.Text;
            string modele = txtModele.Text;
            int annee = (int)numAnnee.Value;
            double prix = (double)numPrix.Value;

            VehiculeCree = cbType.SelectedItem?.ToString() switch
            {
                "Voiture" => new Voiture(marque, modele, annee, prix, (int)numSpecifique.Value),
                "Moto" => new Moto(marque, modele, annee, prix, (int)numSpecifique.Value),
                "Camion" => new Camion(marque, modele, annee, prix, (double)numSpecifique.Value),
                _ => throw new InvalidOperationException("Type non valide")
            };

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur: {ex.Message}", "Saisie invalide", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}