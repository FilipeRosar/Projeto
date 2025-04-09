
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        private int Turno;
        private Cor JogodorAtual;
        public bool Terminada {  get; private set; }
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogodorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }
        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tabuleiro.RetirarPeca(destino);
            tabuleiro.ColocarPeca(p, destino);
        }
        private void ColocarPecas()
        {
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());

        }
    }
}
