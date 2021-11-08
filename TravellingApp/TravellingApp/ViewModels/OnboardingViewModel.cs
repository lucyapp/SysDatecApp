using ScanApp.Models;
using ScanApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ScanApp.ViewModels
{
    public class OnboardingViewModel : MvvmHelpers.BaseViewModel
    {
        private ObservableCollection<OnboardingModel> items;
        private int position;
        private string skipButtonText;

        public OnboardingViewModel()
        {
            SetSkipButtonText("SALTAR");
            InitializeOnBoarding();
            InitializeSkipCommand();
        }

        private void SetSkipButtonText(string skipButtonText)
                => SkipButtonText = skipButtonText;

        private void InitializeOnBoarding()
        {
            Items = new ObservableCollection<OnboardingModel>
            {
                new OnboardingModel
                {
                    Title = "Bienvenidos a ApaBot",
                    Content = "Sr(a), Debes deligenciar el formulario para poder continuar y procesar la información.",
                    ImageUrl = "bot.json",
                    Opcion0=true,
                    Opcion1=false
                },
                new OnboardingModel
                {
                    Title = "1. Datos Personales",
                    Content = "Sr(a),Ingresa tus datos personales para seguir en el proceso y asi poder registrarse.",
                    ImageUrl = "time.svg",
                    Opcion0=false,
                    Opcion1=true
                },
                new OnboardingModel
                {
                    Title = "2. Información personal",
                    Content = "Sr(a), Deberá llenar los items necesarios para que el sistema conozca un poco de ti.",
                    ImageUrl = "visual_data.svg",
                    Opcion0=false,
                    Opcion1=true
                }
            };
        }

        private void InitializeSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    ExitOnBoarding();
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }

        private static void ExitOnBoarding()
        {
            // _ = Application.Current.MainPage.Navigation.PushAsync(new InformacionPersonal());
             _ = Application.Current.MainPage = new InformacionPersonal();
        }

        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }

        private bool LastPositionReached()
            => Position == Items.Count - 1;

        public ObservableCollection<OnboardingModel> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public string SkipButtonText
        {
            get => skipButtonText;
            set => SetProperty(ref skipButtonText, value);
        }

        public int Position
        {
            get => position;
            set
            {
                if (SetProperty(ref position, value))
                {
                    UpdateSkipButtonText();
                }
            }
        }

        private void UpdateSkipButtonText()
        {
            if (LastPositionReached())
            {
                SetSkipButtonText("PRINCIPAL");
            }
            else
            {
                SetSkipButtonText("SALTAR");
            }
        }

        public ICommand SkipCommand { get; private set; }
    }
}