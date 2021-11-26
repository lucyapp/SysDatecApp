﻿using Android.Content;
using LongpressSample.Droid.CustomRenderer;
using ScanApp.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomImage), typeof(CustomImageRenderer))]
namespace LongpressSample.Droid.CustomRenderer
{

    public class CustomImageRenderer : ImageRenderer
    {
        private CustomImage _view;

        public CustomImageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);


            if (e.NewElement != null)
            {
                _view = e.NewElement as CustomImage;
            }

            Control.SoundEffectsEnabled = true;

            Control.LongClickable = true;
            Control.LongClick += (s, ea) =>
            {
                if (_view != null)
                {
                    _view.LongPressedHandler?.Invoke(_view, ea);
                    var command = _view.LongpressCommand;// CustomImage.GetCommand(_view);
                    command?.Execute(_view);

                }
            };

            Control.Clickable = true;
            Control.Click += (s, ea) =>
            {
                if (_view != null)
                {
                    _view.TappedHandler?.Invoke(_view, ea);
                    var command = _view.Command;// CustomImage.GetCommand(_view);
                    command?.Execute(_view);
                }
            };
        }
    }
}