using System;
using System.Collections.Generic;

namespace JogoDaAdivinhacao
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> historico = new List<string>();
            int pontuacao = 1000;
            bool jogar = true;

            while (jogar)
            {
                string escolha = MostrarMenu();
                if (escolha == "3")
                {
                    Console.WriteLine("Obrigado por jogar!");
                    break;
                }
                else if (escolha == "2")
                {
                    ExibirHistorico(historico, pontuacao);
                }
                else if (escolha == "1")
                {
                    int nivel = Dificuldade();
                    int totalDeTentativas = nivel == 1 ? 10 : nivel == 2 ? 5 : 3;
                    int numeroSecreto = new Random().Next(1, 21);
                    HashSet<int> numerosChutados = new HashSet<int>();
                    bool acertou = false;

                    for (int tentativas = 1; tentativas <= totalDeTentativas; tentativas++)
                    {
                        int numeroDigitado = Chute(numerosChutados);
                        if (numeroDigitado == numeroSecreto)
                        {
                            acertou = true;
                            pontuacao += nivel == 1 ? 2 : nivel == 2 ? 4 : 6;
                            historico.Add($"Número Secreto: {numeroSecreto} - Nível: {nivel} - Tentativas: {tentativas}");
                            Console.WriteLine("Parabéns! Você acertou!");
                            break;
                        }
                        else
                        {
                            int penalizacao = Math.Abs(numeroDigitado - numeroSecreto) / 2;
                            pontuacao = Math.Max(0, pontuacao - penalizacao);
                            Console.WriteLine(numeroDigitado > numeroSecreto ? "O número secreto é menor!" : "O número secreto é maior!");
                        }
                    }

                    if (!acertou)
                    {
                        Console.WriteLine($"Você perdeu! O número secreto era {numeroSecreto}");
                    }

                    jogar = Continuar();
                }
            }
        }

        static string MostrarMenu()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Jogo da Adivinhação");
            Console.WriteLine("1 - Jogar");
            Console.WriteLine("2 - Histórico");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opção: ");
            return Console.ReadLine();
        }

        static int Dificuldade()
        {
            Console.WriteLine("Escolha um nível de dificuldade:");
            Console.WriteLine("1 - Fácil (10 tentativas)");
            Console.WriteLine("2 - Médio (5 tentativas)");
            Console.WriteLine("3 - Difícil (3 tentativas)");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static int Chute(HashSet<int> numerosChutados)
        {
            int numeroDigitado;
            while (true)
            {
                Console.Write("Digite um número entre 1 e 20: ");
                if (int.TryParse(Console.ReadLine(), out numeroDigitado) && numeroDigitado >= 1 && numeroDigitado <= 20)
                {
                    if (numerosChutados.Contains(numeroDigitado))
                    {
                        Console.WriteLine("Você já chutou esse número!");
                    }
                    else
                    {
                        numerosChutados.Add(numeroDigitado);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida!");
                }
            }
            return numeroDigitado;
        }

        static void ExibirHistorico(List<string> historico, int pontuacao)
        {
            Console.WriteLine($"Pontuação: {pontuacao}");
            Console.WriteLine("Histórico de Partidas:");
            foreach (var item in historico)
            {
                Console.WriteLine(item);
            }
        }

        static bool Continuar()
        {
            Console.Write("Deseja jogar novamente? (S/N): ");
            return Console.ReadLine().ToUpper() == "S";
        }
    }
}
