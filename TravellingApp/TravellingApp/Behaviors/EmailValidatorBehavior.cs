using System;
using System.Text.RegularExpressions;
using SysDatecScanApp.Helpers;
using Xamarin.Forms;

namespace SysDatecScanApp.Behaviors
{
    public class EmailValidatorBehavior : Behavior<EntryExtender>
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get => (bool) GetValue(IsValidProperty);
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
                IsValid = Regex.IsMatch(e.NewTextValue, EmailRegex, RegexOptions.IgnoreCase,
              TimeSpan.FromMilliseconds(250));
                ((EntryExtender)sender).IsBorderErrorVisible = !IsValid;
            }
          
        }

        protected override void OnDetachingFrom(EntryExtender bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}