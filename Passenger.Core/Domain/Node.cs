using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Node
    {
        public string Adress { get; protected  set; }
        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }

        protected Node()
        {
        }

        protected Node(string adress, double longitude, double latitude)
        {
            SetAdress(adress);
            SetLongitude(longitude);
            SetLatitude(latitude);
        }

        private void SetAdress(string adress)
        {
            if (string.IsNullOrEmpty(adress))
            {
                throw new Exception("Adress can not be empty.");
            }
            if(Adress ==  adress)
            {
                return;
            }
            Adress = adress;
        }

        private void SetLongitude(double longitude)
        {
            if (double.IsNaN(longitude))
            {
                throw new Exception("Latitude must be a number.");
            }
            if(Longitude == longitude)
            {
                return;
            }
            Longitude = longitude;
        }
        private void SetLatitude(double latitude)
        {
            if (double.IsNaN(latitude))
            {
                throw new Exception("Latitude must be a number.");
            }
            if (Latitude == latitude)
            {
                return;
            }
            Latitude = latitude;
        }
    }
}
