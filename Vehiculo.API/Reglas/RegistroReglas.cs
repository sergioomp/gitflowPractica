﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;

namespace Reglas
{
    public class RegistroReglas : IRegistroReglas
    {
        private readonly IRegistroServicio _registroServicio;
        private readonly IConfiguracion _configuracion;

        public RegistroReglas(IRegistroServicio registroServicio, IConfiguracion configuracion)
        {
            _registroServicio = registroServicio;
            _configuracion = configuracion;
        }

        public async Task<bool> VehiculoEstaRegistrado(string placa, string email)
        {
            var resultadoRegistro = await _registroServicio.Obtener(placa);
            return (resultadoRegistro != null && resultadoRegistro.Email == email);
        }
    }
}
