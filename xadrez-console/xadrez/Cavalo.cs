using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return Cor == Cor.Branca ? "♘" : "♞";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != this.Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            int[,] movimentos = new int[,]
            {
        {-1, -2}, {-2, -1}, {-2, 1}, {-1, 2},
        {1, 2}, {2, 1}, {2, -1}, {1, -2}
            };

            for (int i = 0; i < movimentos.GetLength(0); i++)
            {
                int novaLinha = Posicao.Linha + movimentos[i, 0];
                int novaColuna = Posicao.Coluna + movimentos[i, 1];
                Posicao novaPos = new Posicao(novaLinha, novaColuna);

                if (Tab.PosicaoValida(novaPos) && PodeMover(novaPos))
                {
                    mat[novaLinha, novaColuna] = true;
                }
            }

            return mat;
        }

    }
}
