using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaForca.ConsoleApp
{
    internal class Jogo
    {
        private string[] palavras = { "ABACATE", "ABACAXI", "ACEROLA", "AÇAÍ", "ARAÇA", "BACABA", "BACURI", "BANANA", "CAJÁ", "CAJÚ", "CARAMBOLA", "CUPUAÇU", "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO", "MAÇÃ", "MANGABA", "MANGA", "MARACUJÁ", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI", "TANGERINA", "UMBU", "UVA", "UVAIA" };
        private List<char> letrasInformadas = new List<char>();
        private string palavraParaAdivinhar;
        private char[] palavraParaAdivinharArray;
        private char[] palavraParaExibir;
        private int erros = 0;
        private string mensagem = "";

        public void Iniciar()
        {
            SelecionarPalavra();
            Jogar();
        }

        private void SelecionarPalavra()
        {
            Random aleatorio = new Random();
            palavraParaAdivinhar = palavras[aleatorio.Next(0, palavras.Length)];
            palavraParaAdivinharArray = palavraParaAdivinhar.ToCharArray();
            palavraParaExibir = new string('_', palavraParaAdivinhar.Length).ToCharArray();
        }

        private void Jogar()
        {
            while (erros < 5 && new string(palavraParaExibir) != palavraParaAdivinhar)
            {
                Console.Clear();
                Console.WriteLine(mensagem);
                DesenharForca();
                Console.WriteLine("\n" + new string(palavraParaExibir));
                char chute = SolicitarLetra();
                AvaliarChute(chute);
            }

            FinalizarJogo();
        }

        private char SolicitarLetra()
        {
            char chute = ' ';
            bool entradaValida;

            do
            {
                entradaValida = true;
                Console.Write("\nDigite uma letra: ");
                string entrada = Console.ReadLine().ToUpper();

                if (entrada.Length != 1 || !Char.IsLetter(entrada[0]))
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

                if (palavraParaAdivinhar.Contains(chute.ToString()))
                {
                    for (int i = 0; i < palavraParaAdivinhar.Length; i++)
                    {
                        if (palavraParaAdivinharArray[i] == chute && palavraParaExibir[i] != '_')
                        {
                            Console.WriteLine("\n\nVocê já informou essa letra. Por favor, digite outra.");
                            entradaValida = false;
                            break;
                        }
                    }
                }
            } while (!entradaValida);

            letrasInformadas.Add(chute);

            return chute;
        }

        private void AvaliarChute(char chute)
        {
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

        private void FinalizarJogo()
        {
            Console.Clear();
            DesenharForca();

            if (erros < 5)
            {
                Console.WriteLine("\nParabéns, você ganhou! A palavra era " + palavraParaAdivinhar);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nVocê errou todas as tentativas e perdeu. A palavra era " + palavraParaAdivinhar);
                Console.WriteLine("\nReinicie o jogo e tente novamente.");
                Console.ReadLine();
            }
        }

        private void DesenharForca()
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
