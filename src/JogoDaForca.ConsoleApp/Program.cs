using System;
using System.Collections.Generic;

namespace JogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] palavras = { "ABACATE", "ABACAXI", "ACEROLA", "AÇAÍ", "ARAÇA", "BACABA", "BACURI", "BANANA", "CAJÁ", "CAJÚ", "CARAMBOLA", "CUPUAÇU", "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO", "MAÇÃ", "MANGABA", "MANGA", "MARACUJÁ", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI", "TANGERINA", "UMBU", "UVA", "UVAIA" };
            
            // Criando um objetivo 'Aleatorio' para termos a aleatoriedade no jogo
            Random aleatorio = new Random();

            // Seleciona a palavra aleatória
            string palavraParaAdivinhar = palavras[aleatorio.Next(0, palavras.Length)];

            // Converte a palavra para um array de caracteres
            char[] palavraParaAdivinharArray = palavraParaAdivinhar.ToCharArray();

            // Array para exibir cada letra que não foi adivinhada com o underline '_'
            char[] palavraParaExibir = new string('_', palavraParaAdivinhar.Length).ToCharArray();

            // Cria uma lista com as letras informadas pelo usuário
            List<char> letrasInformadas = new List<char>();

            int erros = 0;
            
            // Variavel para a mensagem que será exibida pro usuário
            string mensagem = "";

            while (erros < 5 && new string(palavraParaExibir) != palavraParaAdivinhar)
            {
                // Aqui eu decidi limpar a tela para que o programa não fique muito extenso na exibição
                Console.Clear();

                // Aqui é a mensagem a ser exibida para o usuário (Após a 1 tentativa, pois irá informar a letra e se foi um acerto ou erro)
                Console.WriteLine(mensagem);

                DesenharForca(erros);

                // Exibindo a palavra com o underline em cada letra que não foi adivinhada
                Console.WriteLine("\n" + new string(palavraParaExibir));

                char chute = ' ';

                bool entradaValida;

                do
                {
                    entradaValida = true;
                    Console.Write("\nDigite uma letra: ");

                    // Convertendo a entrada do usuário para maiúscula
                    string entrada = Console.ReadLine().ToUpper();

                    // Verifica se o usuário digitou apenas 1 letra
                    if (entrada.Length != 1 || !Char.IsLetter(entrada[0]))
                    {
                        Console.WriteLine("\n\nPor favor, digite apenas uma letra.");
                        entradaValida = false;
                        continue;
                    }

                    // Armazenando a letra informada na variavel 'chute'
                    chute = entrada[0];

                    if (letrasInformadas.Contains(chute))
                    {
                        Console.WriteLine("\n\nVocê já informou essa letra. Por favor, digite outra.");
                        entradaValida = false;
                    }
                    
                    // Verifica se a entrada é válida de acordo com as regras de acentuação e cedilha
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

                // Adiciona a letra informada na lista das letras informadas
                letrasInformadas.Add(chute);

                // Valida se a letra informada existe na palavra
                if (palavraParaAdivinhar.Contains(chute.ToString()))
                {
                    mensagem = $"\nA letra '{chute}' existe na palavra";

                    // For para exibir a letra no lugar do underline na palavra
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
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nVocê errou todas as tentativas e perdeu. A palavra era " + palavraParaAdivinhar);
                Console.WriteLine("\nReinicie o jogo e tente novamente.");
                Console.ReadLine();
            }
        }

        // Método para desenhar a forca
        static void DesenharForca(int erros)
        {
            // Parte de cima da forca
            Console.WriteLine(" _____________");
            Console.WriteLine(" |/          |");

            // Desenhando o boneco de acordo com os erros
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
            
            // Parte de baixo da forca
            Console.WriteLine(" |");
            Console.WriteLine(" |");
            Console.WriteLine("_|____");
        }
    }
}
