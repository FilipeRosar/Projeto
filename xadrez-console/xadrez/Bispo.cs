using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return Cor == Cor.Branca ? "♗" : "♝";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != this.Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            
            int[] direcaoLinha = { -1, -1, 1, 1 };
            int[] direcaoColuna = { -1, 1, 1, -1 };

            for (int i = 0; i < 4; i++)
            {
                Posicao pos = new Posicao(Posicao.Linha + direcaoLinha[i], Posicao.Coluna + direcaoColuna[i]);

                while (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;

                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                    {
                        break;
                    }

                    pos.DefinirValor(pos.Linha + direcaoLinha[i], pos.Coluna + direcaoColuna[i]);
                }
            }

            return mat;
        }
    }
}
