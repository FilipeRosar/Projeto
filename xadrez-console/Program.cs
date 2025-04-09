

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
                PosicaoXadrez pos = new('c',7);
                Console.WriteLine(pos);

                Console.WriteLine(pos.ToPosicao());
            } 
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}