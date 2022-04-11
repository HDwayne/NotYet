using System.Windows;
using System.Windows.Controls.Primitives;

namespace NotYet
{
    public class Toogle : ToggleButton
    {
        static Toogle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toogle), new FrameworkPropertyMetadata(typeof(Toogle)));
        }
    }
}
