using AccountMS.Exceptions;
using AccountMS.Models;
using AccountMS.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

namespace AccountMS.Services
{
    public class CustomerService : ICustomerService
    {   
       
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        public CustomerService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        { 
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var client = httpClientFactory.CreateClient();
            var clientesUrl = configuration.GetValue<string>("ClientesService:Url");
            var response = await client.GetAsync($"{clientesUrl}/clientes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var clienteJson = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Customer>(clienteJson); // Deserializar el objeto JSON
                return cliente;
            }
            else
            {
                throw new AccountException("Error al obtener el cliente del microservicio.");
            }
        }

        public async Task<Customer> GetCustomerByIdentification(int identification)
        {
            var client = httpClientFactory.CreateClient();
            var clientesUrl = configuration.GetValue<string>("ClientesService:Url");
            var response = await client.GetAsync($"{clientesUrl}/clientes/GetCustomerByIdentification/{identification}");

            if (response.IsSuccessStatusCode)
            {
                var clienteJson = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Customer>(clienteJson); // Deserializar el objeto JSON
                return cliente;
            }
            else
            {
                throw new AccountException("Error al obtener el cliente del microservicio.");
            }
        }

      
    }
}
