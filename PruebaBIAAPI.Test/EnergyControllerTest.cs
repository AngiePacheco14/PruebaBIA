using System.Net;
using System.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using FluentAssertions;

namespace PruebaBIAAPI.Test
{
    public class EnergyControllerTest: IntegrationTestBuilder
    {
        [Fact]
        public void GetEnergyConsumtionPeriodNoExist()
        {
            var datePeriod = new
            {
                Date = new DateTime(2022, 10, 26),
                Period = "otro"
            };
            HttpResponseMessage respuesta = null;
            try
            {
                var date = datePeriod.Date.Date;
                respuesta = this.TestClient.GetAsync($"/api/Energy/CalculateConsumption?Date={date.Year}-{date.Month}-{date.Day}&Period={datePeriod.Period}").Result;
                respuesta.EnsureSuccessStatusCode();
                Assert.True(false, "Deberia fallar"); 
            }
            catch (Exception)
            {
                respuesta.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public void GetEnergyConsumtionDaily()
        {
            var datePeriod = new
            {
                Date = new DateTime(2022, 10, 26),
                Period = "daily"
            };
            var date = datePeriod.Date.Date;

            var carga = this.TestClient.GetAsync($"/api/Energy/CalculateConsumption?Date={date.Year}-{date.Month}-{date.Day}&Period={ datePeriod.Period}").Result;
            carga.EnsureSuccessStatusCode();
            var response = carga.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<List<RespuestaConsultaDto>>(response);

            Assert.NotNull(response);
            Assert.Equal(24, respuestaConsulta.Count());
        }

        [Fact]
        public void GetEnergyConsumtionWeeklyCount()
        {
            var datePeriod = new
            {
                Date = new DateTime(2022, 10, 26),
                Period = "weekly"
            };
            var date = datePeriod.Date.Date;

            var carga = this.TestClient.GetAsync($"/api/Energy/CalculateConsumption?Date={date.Year}-{date.Month}-{date.Day}&Period={datePeriod.Period}").Result;
            carga.EnsureSuccessStatusCode();
            var response = carga.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<List<RespuestaConsultaDto>>(response);

            Assert.NotNull(response);
            Assert.Equal(7, respuestaConsulta.Count());
        }

        [Fact]
        public void GetEnergyConsumtionWeeklyDates()
        {
            var datePeriod = new
            {
                Date = new DateTime(2022, 10, 26),
                Period = "weekly"
            };
            var date = datePeriod.Date.Date;

            var carga = this.TestClient.GetAsync($"/api/Energy/CalculateConsumption?Date={date.Year}-{date.Month}-{date.Day}&Period={datePeriod.Period}").Result;
            carga.EnsureSuccessStatusCode();
            var response = carga.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<List<RespuestaConsultaDto>>(response);

            Assert.NotNull(response);
            var firstDate = new DateTime(2022, 10, 24).ToString("yyyy-MM-dd HH:mm:ss");
            Assert.Equal(firstDate, respuestaConsulta.FirstOrDefault().Meter_Date);
            var lastDate = new DateTime(2022, 10, 30).ToString("yyyy-MM-dd HH:mm:ss");
            Assert.Equal(lastDate, respuestaConsulta.LastOrDefault().Meter_Date);
        }

        [Fact]
        public void GetEnergyConsumtionMonthlyCount()
        {
            var datePeriod = new
            {
                Date = new DateTime(2022, 10, 26),
                Period = "monthly"
            };
            var date = datePeriod.Date.Date;

            var carga = this.TestClient.GetAsync($"/api/Energy/CalculateConsumption?Date={date.Year}-{date.Month}-{date.Day}&Period={datePeriod.Period}").Result;
            carga.EnsureSuccessStatusCode();
            var response = carga.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<List<RespuestaConsultaDto>>(response);

            Assert.NotNull(response);
            Assert.Equal(31, respuestaConsulta.Count());
        }

        [Fact]
        public void GetEnergyConsumtionMonthlyDates()
        {
            var datePeriod = new
            {
                Date = new DateTime(2022, 10, 26),
                Period = "monthly"
            };
            var date = datePeriod.Date.Date;

            var carga = this.TestClient.GetAsync($"/api/Energy/CalculateConsumption?Date={date.Year}-{date.Month}-{date.Day}&Period={datePeriod.Period}").Result;
            carga.EnsureSuccessStatusCode();
            var response = carga.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = System.Text.Json.JsonSerializer.Deserialize<List<RespuestaConsultaDto>>(response);

            Assert.NotNull(response);
            var firstDate = new DateTime(2022, 10, 01).ToString("yyyy-MM-dd HH:mm:ss");
            Assert.Equal(firstDate, respuestaConsulta.FirstOrDefault().Meter_Date);
            var lastDate = new DateTime(2022, 10, 31).ToString("yyyy-MM-dd HH:mm:ss");
            Assert.Equal(lastDate, respuestaConsulta.LastOrDefault().Meter_Date);
        }
    }
}