

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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.tabuleiro);

                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partidaDeXadrez.ExecutarMovimento(origem, destino);

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