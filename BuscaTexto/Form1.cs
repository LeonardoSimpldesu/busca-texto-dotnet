using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BuscaTexto {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e) {
            texto.Text = "";
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this,
               "Busca em Texto - 2026/1\n\nDesenvolvido por:\n72501561 - Leonardo de Souza Almeida\n0009999 - Pedro Alonso\nProf. Virgílio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.",
               "Sobre o trabalho...",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e) {
            // TODO
            // Caixa de diálogo de abrir arquivo com filtro para extensão txt e rtf
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void forçaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto_da_busca = Interaction.InputBox("Digite o texto da busca:", "Força Bruta");

            if (string.IsNullOrEmpty(texto_da_busca)) return;

            int[] forcaBruta = BuscaForcaBruta.Busca(texto_da_busca, texto.Text);

            texto.SelectAll();
            texto.SelectionBackColor = texto.BackColor;

            foreach (int x in forcaBruta)
            {
                texto.Select(x, texto_da_busca.Length);
                texto.SelectionBackColor = Color.Yellow;
            }

            texto.SelectionLength = 0;
        }

        private void rabinKarpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto_da_busca = Interaction.InputBox("Digite o texto da busca:", "Rabin-Karp");
            BuscaRabinKarp.RKSearch(texto_da_busca, texto.Text);
        }

        private void kMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto_da_busca = Interaction.InputBox("Digite o texto da busca:", "KMP");
            BuscaKMP.KMPSearch(texto_da_busca, texto.Text);
        }

        private void boyerMooreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto_da_busca = Interaction.InputBox("Digite o texto da busca:", "Boyer Moore");
            BuscaBoyerMoore.BMSearch(texto_da_busca, texto.Text);
        }
    }
}
