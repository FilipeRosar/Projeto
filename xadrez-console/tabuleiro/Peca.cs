
namespace xadrez_console.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimento { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Tab = tab;
            Cor = cor;
            QteMovimento = 0;
        }
        public void IncrementarQtdMovimentos()
        {
            QteMovimento++;
        }
        public abstract bool[,] MovimentosPossiveis();
        
    }
}
