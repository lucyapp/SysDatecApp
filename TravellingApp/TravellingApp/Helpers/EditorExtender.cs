using Xamarin.Forms;

namespace SysDatecScanApp.Helpers
{
    public class EditorExtender : Editor
    {
        public static readonly BindableProperty IsBorderErrorVisibleProperty =
            BindableProperty.Create(nameof(IsBorderErrorVisible), typeof(bool), typeof(EditorExtender), false,
                BindingMode.TwoWay);

        public static readonly BindableProperty BorderErrorColorProperty =
            BindableProperty.Create(nameof(BorderErrorColor), typeof(Color), typeof(EditorExtender), Color.Transparent,
                BindingMode.TwoWay);

        public static readonly BindableProperty ErrorTextProperty =
            BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(EditorExtender), string.Empty);

        public bool IsBorderErrorVisible
        {
            get => (bool)GetValue(IsBorderErrorVisibleProperty);
            set => SetValue(IsBorderErrorVisibleProperty, value);
        }

        public Color BorderErrorColor
        {
            get => (Color)GetValue(BorderErrorColorProperty);
            set => SetValue(BorderErrorColorProperty, value);
        }

        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }
    }
}