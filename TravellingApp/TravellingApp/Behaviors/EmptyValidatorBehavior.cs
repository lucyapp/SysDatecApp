using SysDatecScanApp.Helpers;
using Xamarin.Forms;

namespace SysDatecScanApp.Behaviors
{
    public class EmptyValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var isValid = !string.IsNullOrWhiteSpace(args.NewTextValue);
            ((EntryExtender) sender).IsBorderErrorVisible = !isValid;
        }
    }

    public class EmptyPassValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var isValid = !string.IsNullOrWhiteSpace(args.NewTextValue);
            ((EntryPasswordExtender) sender).IsBorderErrorVisible = !isValid;
        }
    }
}