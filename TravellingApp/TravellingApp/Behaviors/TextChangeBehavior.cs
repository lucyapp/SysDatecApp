using System;
using SysDatecScanApp.Renderers;
using Xamarin.Forms;

namespace SysDatecScanApp.Behaviors
{
    public class TextChangeBehavior : Behavior<BorderlessEntry>
    {
       
        protected override void OnAttachedTo(BorderlessEntry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(BorderlessEntry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            double result;
            bool isValid = double.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
