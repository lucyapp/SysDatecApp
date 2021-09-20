using Xamarin.Forms;

namespace SysDatecScanApp.Helpers
{
    public class PickerExtender : Picker
    {
        public static readonly BindableProperty IsBorderErrorVisibleProperty =
            BindableProperty.Create(nameof(IsBorderErrorVisible), typeof(bool), typeof(PickerExtender), false,
                BindingMode.TwoWay);

        public static readonly BindableProperty BorderErrorColorProperty =
            BindableProperty.Create(nameof(BorderErrorColor), typeof(Color), typeof(PickerExtender), Color.Transparent,
                BindingMode.TwoWay);

        public static readonly BindableProperty ErrorTextProperty =
            BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(PickerExtender), string.Empty);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(int), typeof(PickerExtender),
                10, BindingMode.Default);



        //public static readonly BindableProperty ImageProperty =
        //    BindableProperty.Create(nameof(Image), typeof(string), typeof(PickerExtender), string.Empty);

        //public string Image
        //{
        //    get => (string)GetValue(ImageProperty);
        //    set => SetValue(ImageProperty, value);
        //}

        public bool IsBorderErrorVisible
        {
            get => (bool) GetValue(IsBorderErrorVisibleProperty);
            set => SetValue(IsBorderErrorVisibleProperty, value);
        }

        public Color BorderErrorColor
        {
            get => (Color) GetValue(BorderErrorColorProperty);
            set => SetValue(BorderErrorColorProperty, value);
        }

        public string ErrorText
        {
            get => (string) GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public int FontSize
        {
            get => (int) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
    }
}