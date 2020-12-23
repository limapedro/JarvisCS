using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai fazer cálculos
    /// </summary>
    class Calculations
    {
        public static void DoCalculation(string calc)
        {
            string simple = calc.Replace("quanto é", ""); // remove o início da frase
            simple = simple.Trim(); // remove espaços em branco

            string[] parts = simple.Split(' '); // dividi em array de string's
            double result = 0.0; // resultado do cálculo
            switch (parts[1]) // casos para a segunda string, ou seja a operação
            {
                case "mais":
                    result = Convert.ToDouble(parts[0]) + Convert.ToDouble(parts[2]); // usando casting
                    break;
                case "menos":
                    result = Convert.ToDouble(parts[0]) - Convert.ToDouble(parts[2]); // usando casting
                    break;
                case "vezes":
                    result = Convert.ToDouble(parts[0]) * Convert.ToDouble(parts[2]); // usando casting
                    break;
                case "dividido":
                    result = Convert.ToDouble(parts[0]) / Convert.ToDouble(parts[3]); // usando casting
                    break;
                case "porcento":
                    result = Convert.ToDouble(parts[0]) * 100 / Convert.ToDouble(parts[3]); // usando casting
                    break;
            }
            result = Math.Round(result, 2);
            Speaker.Speak(result.ToString());
        }
    }
}
