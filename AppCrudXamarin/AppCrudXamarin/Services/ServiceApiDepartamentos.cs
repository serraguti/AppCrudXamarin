using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using AppCrudXamarin.Models;
using System.Linq;
using Newtonsoft.Json;

namespace AppCrudXamarin.Services
{
    public class ServiceApiDepartamentos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDepartamentos
            (IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrls:ApiDepartamentos"];
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Uri uri = new Uri(this.UrlApi + request);
                HttpResponseMessage response =
                    await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "/api/departamentos";
            List<Departamento> departamentos =
                await this.CallApiAsync<List<Departamento>>(request);
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            string request = "/api/departamentos/" + id;
            Departamento departamento =
                await this.CallApiAsync<Departamento>(request);
            return departamento;
        }

        private async Task<int> GetMaxIdDepartamento()
        {
            List<Departamento> departamentos =
                await this.GetDepartamentosAsync();
            return departamentos.Max(x => x.IdDepartamento) + 1;
        }
        
        public async Task InsertDepartamento(string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Departamento dept = new Departamento
                {
                    IdDepartamento = await this.GetMaxIdDepartamento(),
                    Nombre = nombre, 
                    Localidad = localidad
                };
                string json = JsonConvert.SerializeObject(dept);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/departamentos";
                Uri uri = new Uri(this.UrlApi + request);
                await client.PostAsync(uri, content);
            }
        }

        public async Task UpdateDepartamento(Departamento departamento)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/departamentos";
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.PutAsync(uri, content);
            }
        }

        public async Task DeleteDepartamento(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string request = "/api/departamentos/" + id;
                Uri uri = new Uri(this.UrlApi + request);
                await client.DeleteAsync(uri);
            }
        }
    }
}
