using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Settings
{
    public class GeneralSettings
    {
        public GeneralSettings()
        {
            //Set default value
            Name = "z_konstruktora";
        }
        public string Name { get; set; }
    }
}
