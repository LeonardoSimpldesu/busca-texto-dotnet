using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaKMP {
        static int[] next = new int[1000];

        public static void InitNext(String p) {
            int i = 0, j = -1, m = p.Length;
            next[0] = -1;
            while (i < m) {
                while (j >= 0 && p[i] != p[j])
                    j = next[j];
                i++;
                j++;
                next[i] = j;
            }
        }

        public static int KMPSearch(String p, String t) {
            int i = 0, j = 0, m = p.Length, n = t.Length;
            InitNext(p);
            while (j < m && i < n) {
                while (j >= 0 && t[i] != p[j]) {

                    j = next[j];
                }
                i++;
                j++;
            }
            if (j == m)
                return i - m;
            else
                return -1;
        }

        public static int[] Busca(String p, String t, bool caseSensitive = true)
        {
            if (!caseSensitive)
            {
                p = p.ToLower();
                t = t.ToLower();
            }

            int m = p.Length;
            List<int> indiceInicialDeCadaTermoEncontrado = new List<int>();
            String aux, textoAtual = t;
            int chunckAtual = 0;

            do
            {
                int indice = KMPSearch(p, textoAtual);
                if (indice == -1)
                {
                    break;
                }
                indiceInicialDeCadaTermoEncontrado.Add(indice + chunckAtual);
                aux = textoAtual.Remove(0, indice + m);
                chunckAtual += textoAtual.Length - aux.Length;
                textoAtual = aux;
            } while (true);

            return indiceInicialDeCadaTermoEncontrado.ToArray();
        }
    }
}
