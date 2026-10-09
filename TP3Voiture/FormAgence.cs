using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TP3Voiture;

public class FormAgence : Form
{
    private readonly Agence _agence = new Agence();
    private readonly BindingSource _bindingSource = new BindingSource();

    // DataGridView stylisé
    private readonly DataGridView gridVehicules = new DataGridView
    {
        Dock = DockStyle.Fill,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        ReadOnly = true,
        AllowUserToAddRows = false,
        BorderStyle = BorderStyle.None,
        BackgroundColor = Color.FromArgb(245, 247, 250),
        EnableHeadersVisualStyles = false,
        RowHeadersVisible = false,
        RowTemplate = { Height = 35 }
    };

    // Boutons d'action avec couleurs personnalisées
    private readonly Button btnAjouter = CreateStyledButton("➕ Ajouter", Color.FromArgb(40, 167, 69));      // Vert
    private readonly Button btnLouer = CreateStyledButton("🔑 Louer", Color.FromArgb(0, 123, 255));        // Bleu
    private readonly Button btnRetourner = CreateStyledButton("↩️ Retourner", Color.FromArgb(255, 193, 7));  // Jaune
    private readonly Button btnRechercher = CreateStyledButton("🔍 Rechercher", Color.FromArgb(108, 117, 125));// Gris
    private readonly Button btnFiltrer = CreateStyledButton("⚙️ Filtrer", Color.FromArgb(23, 162, 184));    // Cyan

    public FormAgence()
    {
        Text = "Gestion de l'Agence de Location";
        Size = new Size(1000, 620);
        MinimumSize = new Size(850, 500);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
        BackColor = Color.FromArgb(245, 247, 250);

        ApplyDataGridViewStyles();
        InitLayout();
        RegisterEvents();

        _bindingSource.DataSource = _agence.ListerVehicules();
        gridVehicules.DataSource = _bindingSource;
    }

    private static Button CreateStyledButton(string text, Color backColor)
    {
        Button btn = new Button
        {
            Text = text,
            Height = 42,
            Width = 140,
            BackColor = backColor,
            ForeColor = backColor == Color.FromArgb(255, 193, 7) ? Color.Black : Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Margin = new Padding(5),
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderSize = 0;
        
        // Effet de survol (Hover)
        Color hoverColor = ControlPaint.Light(backColor, 0.15f);
        btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
        btn.MouseLeave += (s, e) => btn.BackColor = backColor;

        return btn;
    }

    private void ApplyDataGridViewStyles()
    {
        // Style de l'en-tête
        gridVehicules.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
        gridVehicules.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        gridVehicules.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        gridVehicules.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        gridVehicules.ColumnHeadersHeight = 40;

        // Style des cellules
        gridVehicules.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
        gridVehicules.DefaultCellStyle.SelectionForeColor = Color.White;
        gridVehicules.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        gridVehicules.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);

        // Alternance de couleur des lignes (Zebra effect)
        gridVehicules.RowsDefaultCellStyle.BackColor = Color.White;
        gridVehicules.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 245);
        gridVehicules.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        gridVehicules.GridColor = Color.FromArgb(220, 224, 230);
    }

    private void InitLayout()
    {
        FlowLayoutPanel topPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            Padding = new Padding(12),
            BackColor = Color.White,
            WrapContents = true
        };

        topPanel.Controls.AddRange(new Control[]
        {
            btnAjouter, btnLouer, btnRetourner, btnRechercher, btnFiltrer
        });

        Panel gridContainer = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(15),
            BackColor = Color.FromArgb(245, 247, 250)
        };
        gridContainer.Controls.Add(gridVehicules);

        Controls.Add(gridContainer);
        Controls.Add(topPanel);
    }

    private void RegisterEvents()
    {
        btnAjouter.Click += BtnAjouter_Click;
        btnLouer.Click += BtnLouer_Click;
        btnRetourner.Click += BtnRetourner_Click;
        btnRechercher.Click += BtnRechercher_Click;
        btnFiltrer.Click += BtnFiltrer_Click;
    }

    private Vehicule? GetVehiculeSelectionne()
    {
        if (gridVehicules.CurrentRow != null && gridVehicules.CurrentRow.Index >= 0)
        {
            return gridVehicules.CurrentRow.DataBoundItem as Vehicule;
        }

        MessageBox.Show("Veuillez sélectionner un véhicule dans le tableau.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return null;
    }

    private void RafraichirGrid(object? source = null)
    {
        _bindingSource.DataSource = source ?? _agence.ListerVehicules();
        _bindingSource.ResetBindings(false);
    }

    private void BtnAjouter_Click(object? sender, EventArgs e)
    {
        using (var form = new FormAjoutVehicule())
        {
            if (form.ShowDialog() == DialogResult.OK && form.VehiculeCree != null)
            {
                _agence.AjouterVehicule(form.VehiculeCree);
                RafraichirGrid();
            }
        }
    }

    private void BtnLouer_Click(object? sender, EventArgs e)
    {
        Vehicule? v = GetVehiculeSelectionne();
        if (v == null) return;

        try
        {
            v.Louer();
            RafraichirGrid();
            MessageBox.Show($"Véhicule ID {v.Id} ({v.Marque} {v.Modele}) loué avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erreur de location", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnRetourner_Click(object? sender, EventArgs e)
    {
        Vehicule? v = GetVehiculeSelectionne();
        if (v == null) return;

        try
        {
            v.Retourner();
            RafraichirGrid();
            MessageBox.Show($"Véhicule ID {v.Id} ({v.Marque} {v.Modele}) retourné avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erreur de retour", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnRechercher_Click(object? sender, EventArgs e)
    {
        string marque = Microsoft.VisualBasic.Interaction.InputBox("Entrez la marque à rechercher :", "Recherche par marque");
        if (!string.IsNullOrWhiteSpace(marque))
        {
            RafraichirGrid(_agence.RechercherParMarque(marque.Trim()));
        }
        else
        {
            RafraichirGrid();
        }
    }

    private void BtnFiltrer_Click(object? sender, EventArgs e)
    {
        string type = Microsoft.VisualBasic.Interaction.InputBox("Entrez le type (Voiture, Moto, Camion, ou Tous) :", "Filtrer par type");
        if (!string.IsNullOrWhiteSpace(type))
        {
            RafraichirGrid(_agence.FiltrerParType(type.Trim()));
        }
        else
        {
            RafraichirGrid();
        }
    }
}