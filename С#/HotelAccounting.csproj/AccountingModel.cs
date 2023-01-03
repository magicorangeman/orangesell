using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;
        public double Price
        { 
            get { return price; }
            set
            {
                price = value;
                if (value < 0) throw new ArgumentException();
                Notify(nameof(Price));
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(Total));
            }  
        }
        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                nightsCount = value;
                if (value < 1) throw new ArgumentException();
                Notify(nameof(NightsCount));
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(Total));
            }
        }
        public double Discount
        { 
            get { return discount; }
            set
            {
                discount = value;
                if (value > 100) throw new ArgumentException();
                Notify(nameof(Discount));
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(Total));
            } 
        }
        public double Total
        {
            get { return total; }
            set
            {
                if (value < 0) throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                discount = (1 - total / (price * nightsCount)) * 100;
                Notify(nameof(Discount));
                if (value < 0) throw new ArgumentException();
            }
        }
    }
}