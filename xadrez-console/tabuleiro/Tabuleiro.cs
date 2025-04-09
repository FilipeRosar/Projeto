﻿
namespace xadrez_console.tabuleiro
{
    class Tabuleiro
    {
        public int Linha { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linha, int colunas)
        {
            Linha = linha;
            Colunas = colunas;
            Pecas = new Peca[Linha, Colunas];
        }
    }
}
