using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt01.Util
{
    class Forms
    {
        public static void AddTitle(string titleText, TableLayoutPanel panel, int columnSpan, int rowSize)
        {
            AddTitle(new Font("Arial", 24, FontStyle.Bold), titleText, panel, columnSpan, rowSize);
        }

        public static void AddTitle(Font font, string titleText, TableLayoutPanel panel, int columnSpan, int rowSize)
        {
            Label title = new Label
            {
                Font = font,
                Text = titleText,
                Dock = DockStyle.Fill
            };
            panel.Controls.Add(title);
            panel.SetColumnSpan(title, columnSpan);
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, rowSize)); // storlek på titel-området
        }

        public static DataGridView CreateUnmodifiableDataGridView(int columnCount)
        {
            return new DataGridView
            {
                ColumnCount = columnCount,
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false, // tar bort den första tomma raden längst till vänster
                MultiSelect = false, // inaktiverar multi cell-selektion (eller i detta fall, multi rad-selektion) som annars är möjligt med shift & control
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, // full rad selektion istället för cell-för-cell selektion
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader, // re-sizar kolumner endast efter rubriker, och inte cell-innehål (som produkt-namn)
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
        }
    }
}
