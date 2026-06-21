using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BuscaTexto
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.Text = "";
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
               "Busca em Texto - 2026/1\n\nDesenvolvido por:\n72501561 - Leonardo de Souza Almeida\n72501405 - Pedro Alonso Ferreira Ezequiel\nProf. Virgílio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.",
               "Sobre o trabalho...",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Abrir arquivo de texto";
                openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|Todos os arquivos (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    if (extension == ".rtf")
                    {
                        texto.LoadFile(filePath, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        texto.Text = File.ReadAllText(filePath);
                    }
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LimparDestaque()
        {
            texto.SelectAll();
            texto.SelectionBackColor = texto.BackColor;
            texto.SelectionLength = 0;
        }

        private void DestacarOcorrencias(int[] indices, int tamanho)
        {
            Color[] cores = new Color[] {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.LightGreen,
                Color.LightSkyBlue,
                Color.Cyan,
                Color.Violet
            };

            foreach (int idx in indices)
            {
                texto.Select(idx, tamanho);
                texto.SelectionBackColor = cores[idx % 7];
            }
            texto.SelectionLength = 0;
        }

        private void SubstituirOcorrencias(int[] indices, string textoBusca, string textoSubstituicao)
        {
            for (int i = indices.Length - 1; i >= 0; i--)
            {
                texto.Select(indices[i], textoBusca.Length);
                texto.SelectedText = textoSubstituicao;
            }

            MessageBox.Show(
                $"{indices.Length} ocorrência(s) substituída(s).",
                "Substituição",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ExecutarPesquisa(string titulo, Func<string, string, bool, int[]> algoritmo)
        {
            using (FormPesquisa form = new FormPesquisa(titulo))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                string textoBusca = form.TextoBusca;
                bool caseSensitive = form.CaseSensitive;

                if (string.IsNullOrEmpty(textoBusca)) return;

                int[] resultados = algoritmo(textoBusca, texto.Text, caseSensitive);

                LimparDestaque();

                if (resultados.Length == 0)
                {
                    MessageBox.Show(
                        "Nenhuma ocorrência encontrada.",
                        titulo,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                if (form.Substituir)
                {
                    SubstituirOcorrencias(resultados, textoBusca, form.TextoSubstituicao);
                }
                else
                {
                    DestacarOcorrencias(resultados, textoBusca.Length);
                    MessageBox.Show(
                        $"{resultados.Length} ocorrência(s) encontrada(s).",
                        titulo,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void forçaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutarPesquisa("Força Bruta", BuscaForcaBruta.Busca);
        }

        private void rabinKarpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutarPesquisa("Rabin-Karp", BuscaRabinKarp.Busca);
        }

        private void kMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutarPesquisa("KMP", BuscaKMP.Busca);
        }

        private void boyerMooreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutarPesquisa("Boyer-Moore", BuscaBoyerMoore.Busca);
        }

        private void texto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
