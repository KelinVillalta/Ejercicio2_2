using SignaturePad.Forms;
using Ejercicio2_2.Model;
using Ejercicio2_2.View;
using System;
using System.IO;
using Xamarin.Forms;


namespace Ejercicio2_2
{
    public partial class MainPage : ContentPage
    {
        string valueBase64;
        public MainPage()
        {
            InitializeComponent();
        }
        


        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(name.Text) || String.IsNullOrWhiteSpace(description.Text))
            {
                await DisplayAlert("ERROR", "SE DEBEN DE LLENAR LOS DATOS DE NOMBRE Y DESCRIPCION PARA GUARDAR", "ACEPTAR");
            }
            else
            {
                Stream image = await PadView.GetImageStreamAsync(SignatureImageFormat.Png);
                // var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                valueBase64 = Convert.ToBase64String(data);


                if (String.IsNullOrWhiteSpace(name.Text) || String.IsNullOrWhiteSpace(description.Text))
                {
                    await DisplayAlert("ERROR", "SE DEBEN DE LLENAR LOS DATOS DE NOMBRE Y DESCRIPCION PARA GUARDAR", "ACEPTAR");
                }

                var signatureToSave = new Signatures
                {
                    imageCode = valueBase64,
                    name = name.Text,
                    description = description.Text
                };

                var result = await App.BaseDatos.saveSignature(signatureToSave);


                if (result != 1)
                {
                    await DisplayAlert("AVISO", "OCURRIO UN ERROR, INTENTE DE NUEVO", "ACEPTAR");
                }

                await DisplayAlert("AVISO", "GUARDADO CORRECTAMENTE", "ACEPTAR");
            }


        }

        private async void ViewSignaturesButton_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new SignaturesList());
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            PadView.Clear();
        }
    }
}
