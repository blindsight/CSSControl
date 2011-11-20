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
