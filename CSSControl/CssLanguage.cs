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
using System.Drawing;
using System.Configuration;
using System.Collections;

namespace CSSControl
{
    class CssLanguage
    {
		public Hashtable tokenList;
        //public List<SyntaxToken> tokenList;

        public CssLanguage()
        {
			tokenList = new Hashtable();

            string[] configTokens = ConfigurationManager.AppSettings["css2.1keywords"].Split(',');

            foreach (string token in configTokens)
            {
				tokenList.Add(token, new SyntaxToken(Color.Blue, token));
               // tokenList.Add(new SyntaxToken(Color.Blue, token));
            }
        }
    }
}
