using SysDatecScanApp.Helpers;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SysDatecScanApp.Behaviors
{
    public class PasswordValidationBehavior : Behavior<EntryPasswordExtender>
    {
        private const string PasswordRegex = @"[^\s]$";//"^[a-zA-Z0-9]*$";


        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        protected override void OnAttachedTo(EntryPasswordExtender bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = Regex.IsMatch(e.NewTextValue, PasswordRegex, RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(250));
            if (!IsValid && e.NewTextValue.Length > 0)
            {
                var entry = (EntryPasswordExtender)sender;
                entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        protected override void OnDetachingFrom(EntryPasswordExtender bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
