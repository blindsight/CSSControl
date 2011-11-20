using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CSSControl
{
    struct SyntaxToken
    {
        public Color Color;
        public String Text;

        public SyntaxToken(Color fontColor, string tokenText)
        {
            Color = fontColor;
            Text = tokenText;
        }
    }
}
