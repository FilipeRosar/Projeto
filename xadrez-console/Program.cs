

using xadrez_console.tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partidaDeXadrez = new();

                while (!partidaDeXadrez.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partidaDeXadrez.tabuleiro);
                        Console.WriteLine();
                        Console.WriteLine($"Turno: {partidaDeXadrez.Turno}");
                        Console.WriteLine($"Aguardando jogada: {partidaDeXadrez.JogadorAtual}");

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                        partidaDeXadrez.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partidaDeXadrez.tabuleiro.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partidaDeXadrez.tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaDeXadrez.ValidarPosicaoDeDestino(origem,destino);

                        partidaDeXadrez.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}