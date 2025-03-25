using System;
using System.Collections.Generic;

namespace jogoDaAdivinhacao
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> historico = new List<string>();
            int pontuacao = 1000;
            bool acertou;

            while (true)
            {
                try
                {
                    acertou = false;
                    HashSet<int> numerosChutados = new HashSet<int>(); // Armazena os números já chutados

                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Jogo da Adivinhação");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine($"Pontuação = {pontuacao}");

                    int nivel;
                    do
                    {
                        Console.WriteLine("Escolha um nível de dificuldade: ");
                        Console.WriteLine("1 - Fácil (10 tentativas / Ganha 2 pontos)");
                        Console.WriteLine("2 - Médio (5 tentativas / Ganha 4 pontos)");
                        Console.WriteLine("3 - Difícil (3 tentativas / Ganha 6 pontos)");
                        Console.Write("Digite sua escolha: ");

                    } while (!int.TryParse(Console.ReadLine(), out nivel) || nivel < 1 || nivel > 3);

                    Random geradorDeNumeros = new Random();
                    int numeroSecreto = geradorDeNumeros.Next(1, 21);
                    int totalDeTentativas = nivel == 1 ? 10 : nivel == 2 ? 5 : 3;

                    for (int tentativas = 1; tentativas <= totalDeTentativas; tentativas++)
                    {
                        int numeroDigitado;

                        do
                        {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"Tentativa {tentativas} de {totalDeTentativas}");
                            Console.Write("Digite um número (de 1 à 20) para chutar: ");

                            if (!int.TryParse(Console.ReadLine(), out numeroDigitado) || numeroDigitado < 1 || numeroDigitado > 20)
                            {
                                Console.WriteLine("Número inválido! Digite um número entre 1 e 20.");
                                continue;
                            }

                            if (numerosChutados.Contains(numeroDigitado))
                            {
                                Console.WriteLine("Você já digitou esse número! Escolha outro.");
                                continue;
                            }

                            numerosChutados.Add(numeroDigitado); // Adiciona ao histórico de tentativas
                            break;

                        } while (true);

                        if (numeroDigitado == numeroSecreto)
                        {
                            acertou = true;
                            Console.Clear();
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("Parabéns! Você acertou!");
                            Console.WriteLine("-----------------------------------");

                            pontuacao += nivel == 1 ? 2 : nivel == 2 ? 4 : 6;
                            historico.Add($"Número Secreto: {numeroSecreto} - Nível: {nivel} - Tentativas: {tentativas}");
                            break;
                        }
                        else
                        {
                            int diferenca = Math.Abs(numeroDigitado - numeroSecreto);
                            int penalizacao = diferenca / 2;
                            pontuacao = Math.Max(0, pontuacao - penalizacao);

                            Console.Clear();
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine(numeroDigitado > numeroSecreto
                                ? $"O número secreto é menor que {numeroDigitado}"
                                : $"O número secreto é maior que {numeroDigitado}");
                            Console.WriteLine($"Você perdeu {penalizacao} pontos!");
                            Console.WriteLine("-----------------------------------");
                        }
                    }

                    if (!acertou)
                    {
                        Console.Clear();
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine($"Você perdeu! O número secreto era {numeroSecreto}");
                        Console.WriteLine("-----------------------------------");
                    }

                    Console.WriteLine($"Pontuação = {pontuacao}");
                    Console.WriteLine("Histórico de partidas:");
                    foreach (var item in historico)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine("Deseja continuar? (S/N)");
                    char continuar;
                    do
                    {
                        continuar = Console.ReadLine().ToUpper()[0];
                        if (continuar != 'S' && continuar != 'N')
                        {
                            Console.WriteLine("Opção inválida! Digite S para continuar ou N para sair.");
                        }
                    } while (continuar != 'S' && continuar != 'N');

                    if (continuar == 'N')
                        break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                    Console.WriteLine("Um erro aconteceu no sistema! O jogo foi reiniciado!");
                }
            }
        }
    }
}
