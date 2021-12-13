using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestAppSharePlugin.Interfaces
{
    public interface IShareService
    {
        void Share(string subject, string message, ImageSource imageSource);
    }
}
