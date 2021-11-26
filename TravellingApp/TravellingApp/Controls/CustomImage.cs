using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace ScanApp.CustomControl
{
    public class CustomImage : Image
    {

        public static readonly BindableProperty LongpressCommandProperty =
       BindableProperty.Create(nameof(LongpressCommand), typeof(ICommand), typeof(CustomImage), null);

        public ICommand LongpressCommand
        {
            get { return (ICommand)GetValue(LongpressCommandProperty); }
            set { SetValue(LongpressCommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
      BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomImage), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomImage), (object)null);

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }



        /// <summary>
        /// Long press event.
        /// If the Content or its children have gesture recognizers set, in order to prevent gesture conflicts, it is recommended to set their InputTransparent property to True.
        /// </summary>
        public event EventHandler LongPressed
        {
            add { LongPressedHandler += value; }
            remove { LongPressedHandler -= value; }
        }
        public EventHandler LongPressedHandler;

        /// <summary>
        /// Tap event.
        /// If the Content or its children have gesture recognizers set, in order to prevent gesture conflicts, it is recommended to set their InputTransparent property to True.
        /// </summary>
        public event EventHandler Tapped
        {
            add { TappedHandler += value; }
            remove { TappedHandler -= value; }
        }
        public EventHandler TappedHandler;




    }
}
