using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    partial class DashboardUserControl
    {
        private System.ComponentModel.IContainer components = null;

        // Dashboard controls
        private Panel panelTopCards;
        private Panel panelCharts;

        private Label lblStockOnHand;
        private Label lblLowStock;
        private Label lblExpiry;
        private Label lblWastage;
        private Label lblBilling;
        private Label lblRevenue;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartStock;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBilling;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelTopCards = new System.Windows.Forms.Panel();
            this.panelCharts = new System.Windows.Forms.Panel();

            this.lblStockOnHand = new System.Windows.Forms.Label();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblWastage = new System.Windows.Forms.Label();
            this.lblBilling = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();

            this.chartStock = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBilling = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelTopCards.SuspendLayout();
            this.panelCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBilling)).BeginInit();
            this.SuspendLayout();

            // 
            // panelTopCards
            // 
            this.panelTopCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopCards.Height = 200;
            this.panelTopCards.BackColor = Color.WhiteSmoke;
            this.panelTopCards.Padding = new Padding(20);

            // Add summary labels to top panel
            int cardWidth = 280;
            int cardHeight = 150;
            int spacing = 30;
            int startX = 20;

            // Stock On Hand
            this.lblStockOnHand.Text = "Stock On Hand: 0";
            this.lblStockOnHand.Size = new Size(cardWidth, cardHeight);
            this.lblStockOnHand.BackColor = Color.LightBlue;
            this.lblStockOnHand.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblStockOnHand.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStockOnHand.Location = new Point(startX, 20);

            // Low Stock
            this.lblLowStock.Text = "Low Stock: 0";
            this.lblLowStock.Size = new Size(cardWidth, cardHeight);
            this.lblLowStock.BackColor = Color.LightSalmon;
            this.lblLowStock.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblLowStock.TextAlign = ContentAlignment.MiddleCenter;
            this.lblLowStock.Location = new Point(startX + (cardWidth + spacing), 20);

            // Expiry
            this.lblExpiry.Text = "Expiring Soon: 0";
            this.lblExpiry.Size = new Size(cardWidth, cardHeight);
            this.lblExpiry.BackColor = Color.LightGoldenrodYellow;
            this.lblExpiry.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblExpiry.TextAlign = ContentAlignment.MiddleCenter;
            this.lblExpiry.Location = new Point(startX + 2 * (cardWidth + spacing), 20);

            // Wastage / Damaged
            this.lblWastage.Text = "Wastage/Damaged: 0";
            this.lblWastage.Size = new Size(cardWidth, cardHeight);
            this.lblWastage.BackColor = Color.LightCoral;
            this.lblWastage.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblWastage.TextAlign = ContentAlignment.MiddleCenter;
            this.lblWastage.Location = new Point(startX + 3 * (cardWidth + spacing), 20);

            // Billing Summary
            this.lblBilling.Text = "Billing: 0";
            this.lblBilling.Size = new Size(cardWidth, cardHeight);
            this.lblBilling.BackColor = Color.LightGreen;
            this.lblBilling.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblBilling.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBilling.Location = new Point(startX + 4 * (cardWidth + spacing), 20);

            // Revenue Summary
            this.lblRevenue.Text = "Revenue: 0";
            this.lblRevenue.Size = new Size(cardWidth, cardHeight);
            this.lblRevenue.BackColor = Color.LightSkyBlue;
            this.lblRevenue.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblRevenue.TextAlign = ContentAlignment.MiddleCenter;
            this.lblRevenue.Location = new Point(startX + 5 * (cardWidth + spacing), 20);

            this.panelTopCards.Controls.AddRange(new Control[] {
                lblStockOnHand, lblLowStock, lblExpiry, lblWastage, lblBilling, lblRevenue
            });

            // 
            // panelCharts
            // 
            this.panelCharts.Dock = DockStyle.Fill;
            this.panelCharts.BackColor = Color.White;
            this.panelCharts.Padding = new Padding(20);

            // Chart: Stock Distribution
            this.chartStock.Dock = DockStyle.Top;
            this.chartStock.Height = 300;
            this.chartStock.BackColor = Color.White;
            this.chartStock.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("StockArea"));
            this.chartStock.Titles.Add("Stock Overview");
            this.chartStock.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("StockLegend"));

            // Chart: Revenue
            this.chartRevenue.Dock = DockStyle.Top;
            this.chartRevenue.Height = 300;
            this.chartRevenue.BackColor = Color.White;
            this.chartRevenue.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("RevenueArea"));
            this.chartRevenue.Titles.Add("Revenue Overview");
            this.chartRevenue.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("RevenueLegend"));

            // Chart: Billing Status
            this.chartBilling.Dock = DockStyle.Top;
            this.chartBilling.Height = 300;
            this.chartBilling.BackColor = Color.White;
            this.chartBilling.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("BillingArea"));
            this.chartBilling.Titles.Add("Billing Overview");
            this.chartBilling.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("BillingLegend"));

            this.panelCharts.Controls.AddRange(new Control[] { chartBilling, chartRevenue, chartStock });

            // 
            // DashboardUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelCharts);
            this.Controls.Add(this.panelTopCards);
            this.Name = "DashboardUserControl";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.BackColor = Color.White;

            this.panelTopCards.ResumeLayout(false);
            this.panelCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBilling)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
