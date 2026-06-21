using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaBoyerMoore {
        static int[] skip = new int[256];

        public static void InitSkip(String p) {
            int j, m = p.Length;
            for (j = 0; j < 256; j++)
                skip[j] = m;
            for (j = 0; j < m; j++)
                skip[p[j]] = m - j - 1;
        }

        public static int BMSearch(String p, String t) {
            int i, j, a, m = p.Length, n = t.Length;
            i = m - 1;
            j = m - 1;
            InitSkip(p);
            while (j >= 0) {
                while (t[i] != p[j]) {
                    a = skip[t[i]];
                    i += (m - j > a) ? (m - j) : a;
                    if (i >= n)
                        return -1;
                    j = m - 1;
                }
                i--;
                j--;
            }
            return i + 1;
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
                int indice = BMSearch(p, textoAtual);
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
