﻿using ScanApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ScanApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            //MainPage = new AppShell();
            Plugin.Media.CrossMedia.Current.Initialize();
            MainPage = new NavigationPage(new LoginPage());

        }

        protected override void OnStart()
        {
            VersionTracking.Track();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}