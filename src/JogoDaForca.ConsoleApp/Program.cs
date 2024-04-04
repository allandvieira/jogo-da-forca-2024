using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] palavras = { "ABACATE", "ABACAXI", "ACEROLA", "ACAI", "ARACA", "BACABA", "BACURI", "BANANA", "CAJA", "CAJU", "CARAMBOLA", "CUPUACU", "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO", "MACA", "MANGABA", "MANGA", "MARACUJA", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI", "TANGERINA", "UMBU", "UVA", "UVAIA" };
            Random rand = new Random();
            string palavraParaAdivinhar = palavras[rand.Next(0, palavras.Length)];
            char[] palavraParaAdivinharArray = palavraParaAdivinhar.ToCharArray();
            char[] palavraParaExibir = new string('_', palavraParaAdivinhar.Length).ToCharArray();

            List<char> letrasInformadas = new List<char>();
            int erros = 0;
            string mensagem = "";
            while (erros < 5 && new string(palavraParaExibir) != palavraParaAdivinhar)
            {
                Console.Clear();
                Console.WriteLine(mensagem);
                DesenharForca(erros);
                Console.WriteLine("\n" + new string(palavraParaExibir));
                char chute = ' '; // inicializando a variável chute
                bool entradaValida;
                do
                {
                    entradaValida = true;
                    Console.Write("\nDigite uma letra: ");
                    string entrada = Console.ReadLine().ToUpper();
                    if (entrada.Length != 1)
                    {
                        Console.WriteLine("\n\nPor favor, digite apenas uma letra.");
                        entradaValida = false;
                        continue;
                    }
                    chute = entrada[0];
                    if (letrasInformadas.Contains(chute))
                    {
                        Console.WriteLine("\n\nVocê já informou essa letra. Por favor, digite outra.");
                        entradaValida = false;
                    }
                } while (!entradaValida);
                letrasInformadas.Add(chute);

                if (palavraParaAdivinhar.Contains(chute.ToString()))
                {
                    mensagem = $"\nA letra '{chute}' existe na palavra";
                    for (int i = 0; i < palavraParaAdivinhar.Length; i++)
                    {
                        if (palavraParaAdivinharArray[i] == chute)
                        {
                            palavraParaExibir[i] = chute;
                        }
                    }
                }
                else
                {
                    mensagem = $"\nA letra '{chute}' não existe na palavra";
                    erros++;
                }
            }

            Console.Clear();
            DesenharForca(erros);
            if (erros < 5)
            {
                Console.WriteLine("\nParabéns, você ganhou! A palavra era " + palavraParaAdivinhar);
            }
            else
            {
                Console.WriteLine("\nVocê errou todas as tentativas e perdeu. A palavra era " + palavraParaAdivinhar);
                Console.WriteLine("\nReinicie o jogo e tente novamente.");
                Console.ReadLine();
            }
        }

        static void DesenharForca(int erros)
        {
            Console.WriteLine(" _____________");
            Console.WriteLine(" |/          |");
            switch (erros)
            {
                case 0:
                    Console.WriteLine(" |            ");
                    break;
                case 1:
                    Console.WriteLine(" |           o");
                    break;
                case 2:
                    Console.WriteLine(" |           o");
                    Console.WriteLine(" |          /");
                    break;
                case 3:
                    Console.WriteLine(" |           o");
                    Console.WriteLine(" |          /x");
                    break;
                case 4:
                    Console.WriteLine(" |           o");
                    Console.WriteLine(" |          /x\\");
                    Console.WriteLine(" |            ");
                    break;
                case 5:
                    Console.WriteLine(" |           o");
                    Console.WriteLine(" |          /x\\");
                    Console.WriteLine(" |           x");
                    Console.WriteLine(" |            ");
                    break;
            }
            Console.WriteLine(" |");
            Console.WriteLine(" |");
            Console.WriteLine("_|____");
        }
    }
}
