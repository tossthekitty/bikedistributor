using System.Collections.Generic;

namespace BikeDistributor
{
    public class ReceiptFormatter
    {
        private List<string> _headerFormats = new List<string>();
        private List<string> _lineFormats = new List<string>();
        private List<string> _tailFormats = new List<string>();

        public List<string> HeaderFormats
        {
            get { return _headerFormats; }
            set { _headerFormats = value; }
        }

        public List<string> LineFormats
        {
            get { return _lineFormats; }
            set { _lineFormats = value; }
        }

        public List<string> TailFormats
        {
            get { return _tailFormats; }
            set { _tailFormats = value; }
        }
    }

    /// <summary>
    /// Here are the crazy strings  - you'll never have to 'see' them.
    /// They are mearly used to match up parameter linking within
    /// Order.ProcessReceiptFormat
    /// </summary>
    public static class ReceiptPieces
    {
        public const string Company = "{B8C5F699-B102-47FB-A569-0D22C6F69603}";
        public const string Tax = "{27450B0A-3DE2-4DC9-9486-285BE93B9633}";
        public const string Total = "{B3F3200C-3FC9-4111-9F02-7643F63BDB64}";
        public const string SubTotal = "{A09C1BBA-DCC2-4331-8739-8E305248D0BA}";
        public const string LineQuantity = "{5B2EB003-D65A-4D25-B6FE-6C90E5AB41A7}";
        public const string BikeBrand = "{D859C524-2C33-4D9A-BB64-D44CC97520CE}";
        public const string BikeModel = "{F7F424FD-7687-4334-865B-30EB83F4BF81}";
        public const string LinePrice = "{4C30ADC4-485C-402A-84DF-7267B68BC57F}";
    }

    public class DiscountMark
    {
        public int Price { get; set; }
        public int DiscountAt { get; set; }
        public double DiscountFactor { get; set; }

        public double discount(int quantity)
        {
            return quantity >= DiscountAt
                ? quantity * Price * DiscountFactor
                : quantity * Price;
        }
    }

    public static class OurDiscounts
    {
        public static readonly DiscountMark OneThousand = new DiscountMark()
        {
            Price = 1000,
            DiscountFactor = .9d,
            DiscountAt = 20
        };
        public static readonly DiscountMark TwoThousand = new DiscountMark()
        {
            Price = 2000,
            DiscountFactor = .8d,
            DiscountAt = 10
        };
        public static readonly DiscountMark FiveThousand = new DiscountMark()
        {
            Price = 5000,
            DiscountFactor = .8d,
            DiscountAt = 5
        };
    }

    public class Bike
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public DiscountMark DiscountMarker { get; set; }
    }
}
