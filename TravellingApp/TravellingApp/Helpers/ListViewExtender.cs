using System.Windows.Input;
using Xamarin.Forms;

namespace SysDatecScanApp.Helpers
{
    public class ListViewExtender : ListView
    {
        [System.Obsolete]
        public static BindableProperty ItemClickCommandProperty =
            BindableProperty.Create<ListViewExtender, ICommand>(x => x.ItemClickCommand, null);


        public ListViewExtender()
        {
            ItemTapped += OnItemTapped;
        }

        public ICommand ItemClickCommand
        {
            get => (ICommand)this.GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            SelectedItem = null;
            if (e.Item != null && this.ItemClickCommand != null && this.ItemClickCommand.CanExecute(e))
            {
                ItemClickCommand.Execute(e.Item);
                SelectedItem = null;
            }
        }
    }
}
