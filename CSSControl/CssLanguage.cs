using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Configuration;

namespace CSSControl
{
    class CssLanguage
    {
        public List<SyntaxToken> tokenList;

        public CssLanguage()
        {
            tokenList = new List<SyntaxToken>();

            string[] configTokens = ConfigurationManager.AppSettings["css2.1keywords"].Split(',');

            foreach (string token in configTokens)
            {
                tokenList.Add(new SyntaxToken(Color.Blue, token));
            }
        }
    }
}
