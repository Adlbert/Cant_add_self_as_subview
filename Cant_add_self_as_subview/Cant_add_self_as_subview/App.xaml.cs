using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cant_add_self_as_subview.Services;
using Cant_add_self_as_subview.Views;

namespace Cant_add_self_as_subview {
    public partial class App : Application {

        public App() {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
