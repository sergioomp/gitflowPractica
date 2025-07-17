﻿using System.Net.Http;
using Abstracciones.Interfaces.Reglas;
using System.Text.Json;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Registro;
using Abstracciones.Modelos.Servicios.Revision;
namespace Servicios
{
    public class RevisionServicio : IRevisionServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public RevisionServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<Revision> Obtener(string placa)
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiEndpointsRevision", "ObtenerRevision");
            var servicioRegistro = _httpClient.CreateClient("ServicioRevision");
            var respuesta = await servicioRegistro.GetAsync(string.Format(endPoint, placa));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Revision>>(resultado, opciones);
            return resultadoDeserializado.FirstOrDefault();
        }
    }
}
