using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {

        private static readonly Bike Defy = new Bike()
        {
            Brand = "Giant",
            Model = "Defy 1",
            DiscountMarker = OurDiscounts.OneThousand
        };

        private static readonly Bike Elite = new Bike()
        {
            Brand = "Specialized",
            Model = "Venge Elite",
            DiscountMarker = OurDiscounts.TwoThousand
        };

        private static readonly Bike DuraAce = new Bike()
        {
            Brand = "Specialized",
            Model = "S-Works Venge Dura-Ace",
            DiscountMarker = OurDiscounts.FiveThousand
        };

        private ReceiptFormatter GetNormalFormatter()
        {
            ReceiptFormatter formatter = new ReceiptFormatter();
            formatter.HeaderFormats.Add("Order Receipt for " + ReceiptPieces.Company + Environment.NewLine);
            formatter.LineFormats.Add("\t" + ReceiptPieces.LineQuantity + " x " + ReceiptPieces.BikeBrand + " "
                                      + ReceiptPieces.BikeModel + " = " + ReceiptPieces.LinePrice + Environment.NewLine);
            formatter.TailFormats.Add("Sub-Total: " + ReceiptPieces.SubTotal + Environment.NewLine);
            formatter.TailFormats.Add("Tax: " + ReceiptPieces.Tax + Environment.NewLine);
            formatter.TailFormats.Add("Total: " + ReceiptPieces.Total);
            return formatter;
        }

        private ReceiptFormatter GetHtmlFormatter()
        {
            ReceiptFormatter formatter = new ReceiptFormatter();
            formatter.HeaderFormats.Add(string.Format("<html><body><h1>Order Receipt for {0}</h1>", ReceiptPieces.Company));
            formatter.HeaderFormats.Add("<ul>");

            formatter.LineFormats.Add(string.Format("<li>{0} x {1} {2} = {3}</li>", ReceiptPieces.LineQuantity,
                ReceiptPieces.BikeBrand, ReceiptPieces.BikeModel, ReceiptPieces.LinePrice));

            formatter.TailFormats.Add("</ul>");
            formatter.TailFormats.Add(string.Format("<h3>Sub-Total: {0}</h3>", ReceiptPieces.SubTotal));
            formatter.TailFormats.Add(string.Format("<h3>Tax: {0}</h3>", ReceiptPieces.Tax));
            formatter.TailFormats.Add(string.Format("<h2>Total: {0}</h2>", ReceiptPieces.Total));
            formatter.TailFormats.Add("</body></html>");
            return formatter;
        }

        [TestMethod]
        public void ReceiptOneDefy()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Defy, 1));
            Assert.AreEqual(ResultStatementOneDefy, order.Receipt(GetNormalFormatter()));
        }

        private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void ReceiptOneElite()
        {

            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Elite, 1 ));
            Assert.AreEqual(ResultStatementOneElite, order.Receipt(GetNormalFormatter()));
        }

        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

        [TestMethod]
        public void ReceiptOneDuraAce()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(DuraAce, 1));
            Assert.AreEqual(ResultStatementOneDuraAce, order.Receipt(GetNormalFormatter()));
        }

        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

        [TestMethod]
        public void HtmlReceiptOneDefy()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Defy, 1));
            Assert.AreEqual(HtmlResultStatementOneDefy, order.HtmlReceipt(GetHtmlFormatter()));
        }

        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Elite, 1));
            Assert.AreEqual(HtmlResultStatementOneElite, order.HtmlReceipt(GetHtmlFormatter()));
        }

        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(DuraAce, 1));
            Assert.AreEqual(HtmlResultStatementOneDuraAce, order.HtmlReceipt(GetHtmlFormatter()));
        }

        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";    
    }
}


