using SysDatecScanApp.Helpers;
using Xamarin.Forms;

namespace SysDatecScanApp.Behaviors
{
    public class NumericCelularValidationBehavior : Behavior<EntryExtender>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty MinLengthProperty =
            BindableProperty.Create("MinLength", typeof(int), typeof(NumericCelularValidationBehavior), 0);

        public int MinLength
        {
            get => (int)GetValue(MinLengthProperty);
            set => SetValue(MinLengthProperty, value);
        }
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        protected override void OnAttachedTo(EntryExtender bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                IsValid = int.TryParse(e.NewTextValue, out var result);
                ((EntryExtender)sender).IsBorderErrorVisible = !IsValid;
                IsValid = e.NewTextValue.Length < MinLength;
                ((EntryExtender)sender).IsBorderErrorVisible = IsValid;
            }

        }

        protected override void OnDetachingFrom(EntryExtender bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
