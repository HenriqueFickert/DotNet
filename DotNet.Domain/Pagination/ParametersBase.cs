﻿namespace DotNet.Domain.Pagination
{
    public class ParametersBase
    {
        private const int tamanhoMaximoResultados = 150;

        private int resultadosExibidos = 15;

        private int numeroPagina = 1;

        public int NumeroPagina
        {
            get
            {
                return numeroPagina;
            }
            set
            {
                numeroPagina = (value <= 0) ? numeroPagina : value;
            }
        }

        public int ResultadosExibidos
        {
            get
            {
                return resultadosExibidos;
            }
            set
            {
                resultadosExibidos = value == 0 ? resultadosExibidos : value <= tamanhoMaximoResultados ? value : tamanhoMaximoResultados;
            }
        }
    }
}