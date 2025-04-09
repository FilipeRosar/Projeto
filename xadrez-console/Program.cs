

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
                Tabuleiro tabuleiro = new(8, 8);

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 7));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 3));

                Tela.imprimirTabuleiro(tabuleiro);
            } 
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}