using System;
using Common;

namespace Task6
{
    internal class ChangeFontHandler : Handler
    {
        private Font _fontMask = Font.None;

        protected override string StartInfo => 
            $"Enter:{ Environment.NewLine }" +
            $"\t 1: Bold;{ Environment.NewLine }" +
            $"\t 2: Italic;{ Environment.NewLine }" +
            $"\t 3: Inderline.{ Environment.NewLine }";

        protected override string ResultInfo => "Font parameters:";

        // То самое выражение switch, удобная штука :)
        private Font GetFontByInputOption(int inputOption) =>
            inputOption switch
            {
                1 => Font.Bold,
                2 => Font.Italic,
                3 => Font.Underline,
                _ => throw new ArgumentOutOfRangeException(nameof(inputOption), $"{ nameof(inputOption) } can take values from {{ 1, 2, 3 }}"),
            };

        protected override string HandleData(string data)
        {
            if (int.TryParse(data, out int inputOption))
            {
                try
                {
                    var font = GetFontByInputOption(inputOption);
                    ChangeFont(font);
                    return FontMaskToString(_fontMask);
                }
                catch
                {
                    throw;
                }     
            }
            else
                throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));
        }

        private string FontMaskToString(Font fontMask)
        {
            if (fontMask == 0)
                return Font.None.ToString();

            string result = string.Empty;
            foreach (var f in Enum.GetValues(typeof(Font)))
                if (((Font)f & fontMask) > 0)
                    result += $"{ f }, ";

            return result.TrimEnd(' ', ',');
        }

        private void ChangeFont(Font font) => _fontMask ^= font;
    }
}
