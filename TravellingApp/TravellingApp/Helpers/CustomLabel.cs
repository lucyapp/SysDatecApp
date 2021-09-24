

using Xamarin.Forms;

namespace SysDatecScanApp.Helpers
{
    public class CustomLabel : Label
    {
        public static readonly BindableProperty IsUnderlinedProperty = BindableProperty.Create("IsUnderlined", typeof(bool), typeof(CustomLabel), false, BindingMode.TwoWay, null, OnEventNameChanged);


        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Property changed implementation goes here
        }
        public bool IsUnderlined
        {
            get { return (bool)GetValue(IsUnderlinedProperty); }
            set { SetValue(IsUnderlinedProperty, value); }
        }
    }
}