using Xamarin.Forms;

namespace SysDatecScanApp.Helpers
{
    public class FontAwesomeLabel : Label
    {
        public static readonly string FontAwesomeName = "SysDatecScanApp-icons";

        //Parameterless constructor for XAML
        public FontAwesomeLabel()
        {
            FontFamily = FontAwesomeName;
        }

        public FontAwesomeLabel(string fontAwesomeLabel = null)
        {
            FontFamily = FontAwesomeName;
            Text = fontAwesomeLabel;
        }
    }

    public static class Icon
    {
        public static readonly string FlechaDer = @"\e91d;";
        public static readonly string FAMusic = "\uf001";
        public static readonly string FASearch = "\uf002";
        public static readonly string FAEnvelopeO = "\uf003";
    }
}