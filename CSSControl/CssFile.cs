/*
CSS editor for Windows. Allows CSS to be edited in real time.
    Copyright (C) 2012  Timothy Rhodes <phoenixevolution@gmail.com>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/
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
