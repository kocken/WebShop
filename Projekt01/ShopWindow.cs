using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Projekt01.Util;

namespace Projekt01
{
    class ShopWindow : Form
    {
        EventHandling eventHandling = new EventHandling();

        static public DataGridView productGrid, cartGrid;

        static public Button finalizeOrderButton, removeCartItemButton, clearCartButton;

        static public int selectedSupplyProductId, selectedCartProductId;

        static public bool printedProductAddedInfo = false;

        public ShopWindow()
        {
            Text = "Matbutik";
            Icon = Icon.ExtractAssociatedIcon("Shop.ico");
            Width = 430;
            Height = 450;
            TabControl tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(tabs);
            TabPage productsPage = new TabPage("Sortiment");
            TabPage cartPage = new TabPage("Varukorg");
            tabs.TabPages.Add(productsPage);
            tabs.TabPages.Add(cartPage);
            productsPage.Controls.Add(CreateProductsPanel());
            cartPage.Controls.Add(CreateCartPanel());
        }

        private TableLayoutPanel CreateProductsPanel()
        {
            TableLayoutPanel productsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2
            };
            Forms.AddTitle("Sortiment", productsPanel, productsPanel.ColumnCount, 36);
            productGrid = CreateProductGrid();
            productGrid.SelectionChanged += eventHandling.ProductGridSelection;
            productsPanel.Controls.Add(productGrid);
            foreach (Product product in Program.Products)
            {
                ShowProductGridItem(product);
            }
            Button addCartItemButton = new Button()
            {
                Text = "Lägg till",
                Margin = new Padding(0, 25, 0, 0) // margin för att placera knappen i höjd med datagriddens första rad, istället för dess rubriker
            };
            addCartItemButton.Click += eventHandling.AddCartItem;
            productsPanel.Controls.Add(addCartItemButton);
            productsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85)); // gör så att DataGridView-objektet får tillräckligt mycket plats att fullt visas
            return productsPanel;
        }

        private TableLayoutPanel CreateCartPanel()
        {
            TableLayoutPanel cartPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 5
            };
            Forms.AddTitle("Varukorg", cartPanel, cartPanel.ColumnCount, 36);
            cartGrid = CreateProductGrid();
            cartGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Antal"
            });
            cartGrid.SelectionChanged += eventHandling.CartGridSelection;
            cartPanel.SetColumnSpan(cartGrid, cartPanel.ColumnCount);
            cartPanel.Controls.Add(cartGrid);
            cartPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 311)); // höjd på grid
            finalizeOrderButton = CreateDisabledCartButton("Beställ");
            removeCartItemButton = CreateDisabledCartButton("Ta bort");
            clearCartButton = CreateDisabledCartButton("Rensa");
            finalizeOrderButton.Click += eventHandling.FinalizeOrder;
            removeCartItemButton.Click += eventHandling.RemoveCartItem;
            clearCartButton.Click += delegate (object sender, EventArgs e) { // delegate-delen gör så att eventets argument inte behöver skickas till #ClearCart metoden
                ClearCart();
            };
            cartPanel.Controls.Add(finalizeOrderButton);
            cartPanel.Controls.Add(removeCartItemButton);
            cartPanel.Controls.Add(clearCartButton);
            return cartPanel;
        }

        private void ShowProductGridItem(Product product)
        {
            productGrid.Rows.Add(
                product.Id,
                product.Name,
                product.Price.ToString("0.00"), // pris med två decimaltal
                product.EcoLabelled // ekologisk produkt värde för checkbox
            );
        }

        static public void ShowCartGridItem(ShopItem cartItem)
        {
            foreach (DataGridViewRow row in cartGrid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(cartItem.Id.ToString())) // om produkten (dess id) finns listad i varukorgen
                {
                    row.Cells[row.Cells.Count - 1].Value = cartItem.Amount; // modifiera raden som redan finns istället för att lägga till en ny rad
                    return;
                }
            }
            cartGrid.Rows.Add( // om produkten inte redan är listad i gridden, lägg till en ny rad
                cartItem.Id,
                cartItem.Name,
                cartItem.Price.ToString("0.00"),
                cartItem.EcoLabelled,
                cartItem.Amount
            );
        }

        static public void ClearCart()
        {
            Program.Cart.Clear();
            cartGrid.Rows.Clear();
            DisableCartButtons();
        }

        static public void DisableCartButtons()
        {
            finalizeOrderButton.Enabled = false;
            removeCartItemButton.Enabled = false;
            clearCartButton.Enabled = false;
        }

        private DataGridView CreateProductGrid()
        {
            DataGridView grid = Forms.CreateUnmodifiableDataGridView(3);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Produkt";
            grid.Columns[2].HeaderText = "Pris (kr)";
            grid.Columns.Add(new DataGridViewCheckBoxColumn // fjärde kolumn
            {
                Name = "Ekologisk" // kolumn-header
            });
            return grid;
        }

        private Button CreateDisabledCartButton(string text)
        {
            return new Button()
            {
                Text = text,
                Enabled = false,
                Margin = new Padding(2, 7, 8, 0) // margin för att placera knapparna snyggt vid fönstrets botten-kant
            };
        }
    }
}
