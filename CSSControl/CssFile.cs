using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSSControl
{
    class CssFile
    {
        string href;
        string fileLocation;
        bool overRide;
        public int index;
        public String name;

        public void overrideLocation(string overrideLocation)
        {
            href = overrideLocation;

            if(!overrideLocation.Equals("")) {
                overRide = true;
            } else {
                overRide = false;
            }
        }

        public Boolean doesOverRide()
        {
            return overRide;
        }

        public String Href
        {
            get { return href; }
            set { href = value; }
        }

        public String FileLocation
        {
            get { return fileLocation; }
            set { fileLocation = value; }
        }
    }
}
