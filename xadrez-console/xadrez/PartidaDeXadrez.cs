
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tabuleiro.RetirarPeca(destino);
            tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            // JogadaEspecial: Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tabuleiro.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, destinoT);
            }
            // JogadaEspecial: Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tabuleiro.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, destinoT);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tabuleiro.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            tabuleiro.ColocarPeca(p, origem);
            // JogadaEspecial: Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tabuleiro.RetirarPeca(destinoT);
                T.DecrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, origemT);
            }
            // JogadaEspecial: Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tabuleiro.RetirarPeca(destinoT);
                T.DecrementarQtdMovimentos();
                tabuleiro.ColocarPeca(T, origemT);
            }

        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != tabuleiro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem não é sua!");
            }
            if (!tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça escolhida!");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }

            }
            return false;
        }
        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(x.Posicao, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {

            ColocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branca));
            ColocarNovaPeca('c',1, new Bispo(tabuleiro,Cor.Branca));
            ColocarNovaPeca('d', 1, new Rainha(tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branca));
            ColocarNovaPeca('a',2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branca));

            ColocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rainha(tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preta));


        }
    }
}
