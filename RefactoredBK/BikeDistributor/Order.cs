using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
    public class Order
    {
        private const double TaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();

        private double SubTotal { get; set; }
        private double Taxes { get; set; }
        private double Total { get; set; }

        public string Company { get; private set; }

        public Order(string company)
        {
            Company = company;
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string HtmlReceipt(ReceiptFormatter formatter)
        {
            return ReceiptProcessor(formatter);
        }

        public string Receipt(ReceiptFormatter formatter)
        {
            return ReceiptProcessor(formatter);
        }

        /// <summary>
        /// We really only need this - all the string formation is the 
        /// responsibility of the caller now.  I left the two methods 'HtmlReceipt and Receipt' 
        /// to maintain some consistancy to the original orderTest
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        private string ReceiptProcessor(ReceiptFormatter formatter)
        {
            SubTotal = Taxes = Total = 0;
            var result = new StringBuilder();
            
            foreach (string headerFormat in formatter.HeaderFormats)
            {
                result.Append(ProcessReceiptFormat(headerFormat));
            }

            foreach (var line in _lines)
            {
                var thisAmount = line.Bike.DiscountMarker.discount(line.Quantity);
                foreach (string lineFormat in formatter.LineFormats)
                {
                    result.Append(ProcessReceiptFormat(lineFormat, line));
                }
                SubTotal += thisAmount;
            }
            Taxes = SubTotal * TaxRate;
            Total = SubTotal + Taxes;
            for (int i = 0; i < formatter.TailFormats.Count - 1; i++)
            {
                result.Append(ProcessReceiptFormat(formatter.TailFormats[i]));
            }
            result.Append(ProcessReceiptFormat(formatter.TailFormats.Last()));
            return result.ToString();
        }

        /// <summary>
        /// Here is the *MAGIC TRICK* !
        /// Rather than attempting align parameters via string formats - we use
        /// MAGIC STRINGS that no one in their right mind would ever use; defined
        /// from the ReceiptPieces class. 
        /// 
        /// The conundrum to this approach is ; Any data extracted for receipts
        /// must be 1) Labeled in ReceiptPieces and 2) Addressed to proper assignment
        /// here.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private string ProcessReceiptFormat(string input, Line line = null)
        {
            string output = input.Replace(ReceiptPieces.Company, Company);
            if (line != null)
            {
                output = output.Replace(ReceiptPieces.LinePrice, line.Bike.DiscountMarker.discount(line.Quantity).ToString("C"));
                output = output.Replace(ReceiptPieces.LineQuantity, line.Quantity.ToString());
                output = output.Replace(ReceiptPieces.BikeBrand, line.Bike.Brand);
                output = output.Replace(ReceiptPieces.BikeModel, line.Bike.Model);
            }

            output = output.Replace(ReceiptPieces.Total, Total.ToString("C"));
            output = output.Replace(ReceiptPieces.SubTotal, SubTotal.ToString("C"));
            output = output.Replace(ReceiptPieces.Tax, Taxes.ToString("C"));
            
            return output;
        }

    }
}
