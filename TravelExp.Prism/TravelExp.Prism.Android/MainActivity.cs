﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using Syncfusion.SfBusyIndicator.XForms.Droid;

namespace TravelExp.Prism.Droid
{
    [Activity(Label = "TravelExp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            new SfBusyIndicatorRenderer();
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }



    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

