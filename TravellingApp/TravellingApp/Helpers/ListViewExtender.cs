using System.Windows.Input;
using Xamarin.Forms;

namespace ScanApp.Helpers
{
    public class ListViewExtender : ListView
    {
        [System.Obsolete]
        public static BindableProperty ItemClickCommandProperty =
            BindableProperty.Create<ListViewExtender, ICommand>(x => x.ItemClickCommand, null);

        [System.Obsolete]
        public ListViewExtender()
        {
            ItemTapped += OnItemTapped;
        }

        [System.Obsolete]
        public ICommand ItemClickCommand
        {
            get => (ICommand)this.GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

        [System.Obsolete]
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            SelectedItem = null;
            if (e.Item != null && ItemClickCommand != null && ItemClickCommand.CanExecute(e))
            {
                ItemClickCommand.Execute(e.Item);
                SelectedItem = null;
            }
        }
    }
}
