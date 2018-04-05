using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt01.Util
{
    class EventHandling
    {
        public void ProductGridSelection(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            DataGridViewSelectedRowCollection selectedRows = grid.SelectedRows;
            if (selectedRows.Count > 0) // finns markerade rader (dock bara en rad vald, då multi-selektion är false)
            {
                ShopWindow.selectedSupplyProductId = Int32.Parse(selectedRows[0].Cells[0].Value.ToString()); // lagrar den markerade radens produkt-ID
            }
            else // ingen rad är vald

            {
                ShopWindow.selectedSupplyProductId = -1;
            }
        }

        public void CartGridSelection(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            DataGridViewSelectedRowCollection selectedRows = grid.SelectedRows;
            if (selectedRows.Count > 0)
            {
                ShopWindow.selectedCartProductId = Int32.Parse(selectedRows[0].Cells[0].Value.ToString());
                ShopWindow.removeCartItemButton.Enabled = true;
            }
            else
            {
                ShopWindow.selectedCartProductId = -1;
                ShopWindow.removeCartItemButton.Enabled = false;
            }
        }

        public void AddCartItem(object sender, EventArgs e)
        {
            if (ShopWindow.selectedSupplyProductId >= 0) // den "selectade" radens produkt-id (i sortiment-gridden, d.v.s. "productGrid")
            {
                if (!Program.Cart.ContainsKey(ShopWindow.selectedSupplyProductId)) // om produkten inte har blivit tillagd i varukorgen förrut
                {
                    Program.Cart.Add(
                        ShopWindow.selectedSupplyProductId, // produktens id är nyckel i dictionaryt
                        new ShopItem
                        (
                            id: ShopWindow.selectedSupplyProductId,
                            name: Program.Products[ShopWindow.selectedSupplyProductId].Name,
                            price: Program.Products[ShopWindow.selectedSupplyProductId].Price,
                            ecoLabelled: Program.Products[ShopWindow.selectedSupplyProductId].EcoLabelled,
                            amount: 1
                        )
                    );
                }
                else // om produkten har lagts till i varukorgen förrut
                {
                    Program.Cart[ShopWindow.selectedSupplyProductId].Amount++; // öka antal med 1
                }
                ShopWindow.ShowCartGridItem(Program.Cart[ShopWindow.selectedSupplyProductId]); // visar upp objektets data i kundvagn-gridden ("cartGrid")
                ShopWindow.finalizeOrderButton.Enabled = true; // finns en eller flera produkter i kundvagnen; aktivera order och clear knapparna
                ShopWindow.clearCartButton.Enabled = true;
                if (!ShopWindow.printedProductAddedInfo)
                {
                    Button button = (Button)sender;
                    MessageBox.Show("Produkt tillagd till varukorg." + "\n\n" +
                        "Fortsätt att lägga till fler produkter, eller gå till varukorg-fliken för att beställa eller ta bort produkter från varukorgen.", 
                        "Information");
                    ShopWindow.printedProductAddedInfo = true;
                }
            }
            else
            {
                MessageBox.Show("Kan inte lägga till produkt, ingen produkt är vald", "Information");
            }
            ShopWindow.productGrid.Focus(); // ge tillbaka fokus till produkt-grid för smidigare användarupplevelse
        }

        public void FinalizeOrder(object sender, EventArgs e)
        {
            Discount selectedDiscount = Input.GetDiscount(Program.Discounts);
            double totalCost = 0;
            string orderInfo = "Order slutförd." + "\n\n" + "Beställning:";
            foreach (ShopItem item in Program.Cart.Values)
            {
                totalCost += item.Price * item.Amount;
                orderInfo += "\n" + item.Summary;
            }
            if (selectedDiscount != null) // sant om en giltig rabattkod används, om inte så är objektet null
            {
                totalCost = selectedDiscount.DiscountedValue(totalCost);
            }
            totalCost = Math.Round(totalCost, 2, MidpointRounding.AwayFromZero); // rundar till två decimaler (ifall rabattkoden gjorde så att värdet fick många decimaler)
            orderInfo += "\n\n" + "Totalsumma: " + "\n" + totalCost + "kr" +
                (selectedDiscount != null ? " (" + selectedDiscount.Percent + "% rabatt)" : "");
            MessageBox.Show(orderInfo, "Information");
            ShopWindow.ClearCart();
        }

        public void RemoveCartItem(object sender, EventArgs e)
        {
            if (ShopWindow.selectedCartProductId >= 0) // den "selectade" radens produkt-id (i kundvagn-gridden, d.v.s. "cartGrid")
            {
                Program.Cart[ShopWindow.selectedCartProductId].Amount--; // minska antal av kundvagn-objektet med ett
                if (Program.Cart[ShopWindow.selectedCartProductId].Amount <= 0) // om objektets antal är 0 (eller mindre för failsafe) efter minskning
                {
                    Program.Cart.Remove(ShopWindow.selectedCartProductId); // tar bort objektet från kundvagn-dictionaryt
                    foreach (DataGridViewRow row in ShopWindow.cartGrid.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(ShopWindow.selectedCartProductId.ToString())) // cell där produkten (dess id) är listad
                        {
                            ShopWindow.cartGrid.Rows.Remove(row); // ta bort rad från grid, då antal är under 1 och ej bör visas
                            break;
                        }
                    }
                }
                else // om objektets antal är 1 eller högre efter minskning
                {
                    ShopWindow.ShowCartGridItem(Program.Cart[ShopWindow.selectedCartProductId]); // uppdaterar produkt-raden i kundvagn-gridden med det nya antalet
                }
                if (Program.Cart.Count == 0) // om inga objekt längre finns i varukorgen, d.v.s. om vi just tog bort det enda objektet
                {
                    ShopWindow.DisableCartButtons();
                }
            }
        }
    }
}
