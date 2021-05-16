using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.Msal;
using Xamarin.Forms;


namespace Samples
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(IDialogs dialogs)
        {
            IMsalService msal = null;
            this.Title = "Framework";
            this.Button = ReactiveCommand.CreateFromTask(async () => await dialogs.Alert("HI"));

            this.MsalSignIn = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await msal.SignIn();
                await dialogs.Snackbar("Sign-In Result: " + result);
            });

            this.MsalSignOut = ReactiveCommand.CreateFromTask(async () =>
            {
                await msal.SignOut();
                await dialogs.Snackbar("Sign-Out success");
            });

            this.MsalRefresh = ReactiveCommand.CreateFromTask(async () =>
            {
                await msal.TryRefresh();
                await dialogs.Snackbar("Refresh success");
            });

            this.WhenAnyValue(x => x.Text1IsFocused)
                .Select(focused => focused ? Color.Red : Color.Black)
                .Subscribe(color => {
                    this.Text1Color = color;
                })
                .DisposedBy(this.DestroyWith);

            this.WhenAnyValue(x => x.Text2IsFocused)
                .Select(focused => focused ? Color.Red : Color.Black)
                .Subscribe(color => {
                    this.Text2Color = color;
                })
                .DisposedBy(this.DestroyWith);

            this.Toggle1 = ReactiveCommand.Create(() => this.Text1IsFocused = !this.Text1IsFocused);
            this.Toggle2 = ReactiveCommand.Create(() => this.Text2IsFocused = !this.Text2IsFocused);
        }


        public ICommand Button { get; }
        public ICommand MsalSignIn { get; }
        public ICommand MsalSignOut { get; }
        public ICommand MsalRefresh { get; }

        public ICommand Toggle1 { get; }
        [Reactive] public string Text1 { get; set; }
        [Reactive] public bool Text1IsFocused { get; set; }
        [Reactive] public Color Text1Color { get; set; }

        public ICommand Toggle2 { get; }
        [Reactive] public string Text2 { get; set; }
        [Reactive] public bool Text2IsFocused { get; set; }
        [Reactive] public Color Text2Color { get; set; }
    }
}
