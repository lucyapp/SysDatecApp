using Xamarin.Forms;

namespace ScanApp.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;
            var isValid = int.TryParse(args.NewTextValue, out var result);
            entry.TextColor = isValid ? Color.Default : Color.Red;
            entry.Text = isValid && int.Parse(entry.Text) < 11 ? entry.Text : "";

            // if Entry text is longer then valid length
            if (entry.Text.Length > MaxLength && isValid)
            {
                var entryText = entry.Text;

                entryText = entryText.Remove(entryText.Length - 1); // remove last char

                entry.Text = entryText;
            }
        }
    }
}