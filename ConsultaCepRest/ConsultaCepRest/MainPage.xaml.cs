using ConsultaCepRest.Servico;
using ConsultaCepRest.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConsultaCepRest
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
		}

        private async void btnConsultar_ClickedAsync(object sender, EventArgs e)
        {
            actIndLoading.IsRunning = true;
            actIndLoading.IsVisible = true;

            string cep = txtCep.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    //Endereco end = await ViaCepService.BuscaEndereco(cep);
                    Endereco end = await Task.Run(async () => 
                    {
                        return await ViaCepService.BuscaEndereco(cep);
                    });

                    if (end != null)
                    {
                        //lblResultado.Text = string.Format("Endereco: {0},{1} - {2} - {3} - {4} - {5}", end.Logradouro, end.Complemento, end.Bairro, end.Localidade, end.Uf, end.Cep);
                        lblResultado.Text = $"Endereço: {end.Logradouro}, {end.Complemento} \nBairro: {end.Bairro} \nCidade: {end.Localidade} \nEstado: {end.Uf} \nCEP: {end.Cep}";
                    }
                    else
                    {
                        await DisplayAlert("ERRO", $"Endereço não encontrado para o CEP informado: {cep}","OK");
                    }

                    actIndLoading.IsRunning = false;
                    actIndLoading.IsVisible = false;
                }
                catch (Exception exception)
                {
                    await DisplayAlert("ERRO CRÍTICO",exception.Message,"OK");
                    actIndLoading.IsRunning = false;
                    actIndLoading.IsVisible = false;
                    throw;
                }
            }
        }

	    private bool isValidCEP(string cep)
	    {
	        bool valido = true;

            if (cep.Length != 8)
            {
                //erro
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            int novoCep = 0;
	        if (!int.TryParse(cep, out novoCep))
	        {
                //erro
	            DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
	        }

	        return valido;
	    }
    }
}
