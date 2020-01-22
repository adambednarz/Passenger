using System;

namespace Passenger.Core.Domain
{
    public class Vehicle    // ValueObject -> imutable
    {
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public int Seats { get; protected set; }

        protected Vehicle()
        {
        }
        protected Vehicle(string brand, string name, int seats)
        {
            SetBrand(brand);
            SetName(name);
            SetSeats(seats);
        }

        public static Vehicle Create(string brand, string name, int seats)
            => new Vehicle(brand, name, seats);

        private void SetBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new Exception("Please provie valid data.");
            }
            if (Brand == brand)
            {
                return;
            }
            Brand = brand;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Please provie valid data.");
            }
            if (Model == name)
            {
                return;
            }
            Model= name;
        }

        private void SetSeats(int seats)
        {
            if(seats < 0)
            {
                throw new Exception("Seats must be grater than 0.");
            }
            else if (seats > 9)
            {
                throw new Exception("You can not provide more than 9 seats");
            }
            else if(Seats == seats)
            {
                return;
            }
            Seats = seats;
        }
    }
}
