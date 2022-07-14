﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;


namespace Wordle
{


    public class JuegoWordle
    {
        public string nombre, palabra;
        public int intentos = 0, dificultad = 0, voucher = 0;
        public int maxIntentos = 5;
        public bool juegoTerminado = false, partidaGanada = false;
        public Dictionary<string, int> puntajes = new Dictionary<string, int>() { };
        public List<string> palabrasIntentadas = new List<string>();
        public List<string> resultadoIntentos = new List<string>();
        Stopwatch stopWatch = new Stopwatch();
        public string elapsedTime = "0";

        public string[] palabrasFacil = new string[] { "AUTO", "CASA", "PATO", "LORO" };
        public string[] palabrasMedio = new string[] { "BORDES", "CARROS", "CARCEL", "DISCOS" };
        public string[] palabrasDificil = new string[] { "CRUCEROS", "SIMBOLOS", "CREDITOS", "CERROJOS" };

        public JuegoWordle(string _nombre, int _maxIntentos, int _dificiultad)
        {
            nombre = _nombre;
            maxIntentos = _maxIntentos;
            dificultad = _dificiultad;
            if (nombre == null || maxIntentos == 0 || dificultad == 0)
            {
                return;
            }
            if (puntajes.TryGetValue(nombre, out _))
            { }
            else
            {
                puntajes.Add(nombre, 0);
            }
            SetPalabra();
            stopWatch.Start();
        }

        private void SetPalabra()
        {
            Random random = new Random();

            if (dificultad <= 1)
            {
                int i = random.Next(0, palabrasFacil.Length);
                palabra = palabrasFacil[i];
            }
            if (dificultad == 2)
            {
                int i = random.Next(0, palabrasMedio.Length);
                palabra = palabrasMedio[i];
            }
            if (dificultad >= 3)
            {
                int i = random.Next(0, palabrasDificil.Length);
                palabra = palabrasDificil[i];
            }

        }

        public bool IntentarPalabra(string _palabra)
        {
            intentos++;
            palabrasIntentadas.Add(_palabra); // añado la palabra a la lista de intentos
            VerificarPalabra(_palabra);
            if (intentos <= maxIntentos)
            {
                if (palabra == _palabra)
                {
                    partidaGanada = true;
                    TerminarJuego();
                    //puntajes[nombre] += 1;
                    //juegoTerminado = true;
                    //stopWatch.Stop();
                    //TimeSpan ts = stopWatch.Elapsed;
                    //elapsedTime = String.Format("{0:00}hs:{1:00}min:{2:00}seg.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
                    //Console.WriteLine(elapsedTime);
                    return true;
                }
                else
                {
                    if (intentos == maxIntentos)
                    {
                        TerminarJuego();
                        //juegoTerminado = true;
                        //puntajes[nombre] -= 1;
                        //stopWatch.Stop();
                    }
                    return false;
                }
            }

            return false;

        }
        private void TerminarJuego()
        {
            puntajes[nombre] = partidaGanada ? puntajes[nombre] + 1 : puntajes[nombre] - 1;
            if (partidaGanada)
            {
                Random random = new Random();
                voucher = random.Next(1000000, 9999999);
            }
            else
            {
                voucher = 0;
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}hs:{1:00}min:{2:00}seg.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            juegoTerminado = true;
        }

        private void VerificarPalabra(string _palabra)
        {

            string palabraIntentada;

            //palabraIntentada = palabrasIntentadas[intentos - 1];
            palabraIntentada = _palabra;

            char[] resultadoIntentado = new char[palabraIntentada.Length];


            for (int i = 0; i < palabraIntentada.Length; i++)
            {
                if (palabraIntentada[i] == palabra[i])
                {
                    resultadoIntentado[i] = 'O';
                }
                else
                {
                    for (int j = 0; j < palabraIntentada.Length; j++)
                    {
                        if (palabraIntentada[i] == palabra[j])
                        {
                            resultadoIntentado[i] = '-';
                            break;
                        }
                        else
                        {
                            resultadoIntentado[i] = 'X';
                        }
                    }
                }

            }
            //Console.WriteLine(resultadoIntentado);
            string res = new string(resultadoIntentado);
            resultadoIntentos.Add(res);
        }

    }
}