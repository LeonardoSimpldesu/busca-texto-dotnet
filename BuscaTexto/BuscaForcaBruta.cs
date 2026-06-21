using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaForcaBruta {
        public static int ForcaBruta(String p, String t) {
            int i, j, aux;
            int m = p.Length;
            int n = t.Length;
            for (i = 0; i < n; i++) {
                aux = i;
                for (j = 0; j < m && aux < n; j++) {
                    if (p[j] == '?')
                    {
                        aux++;
                        continue;
                    }
                    if (t[aux] != p[j])
                        break;
                    aux++;
                }
                if (j == m)
                    return i;
            }
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
            List<int> indiceInicialDeCadaTermoEncontrado = new List<int> { };
            String aux, textoAtual = t;
            int chunckAtual = 0;

            do
            {
                int indice = ForcaBruta(p, textoAtual);
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
