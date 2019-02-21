using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ConsultaCepRest.Servico.Modelo;
using Newtonsoft.Json;

namespace ConsultaCepRest.Servico
{
    public class ViaCepService
    {
        private static string UrlBase = "https://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscaEndereco(string cep)
        {
            string url = string.Format(UrlBase, cep);

            WebClient ws = new WebClient();
            string retorno = ws.DownloadString(url);

            Endereco end =  JsonConvert.DeserializeObject<Endereco>(retorno);

            if (end.Cep == null)
            {
                return null;
            }

            return end;
        }
    }
}
