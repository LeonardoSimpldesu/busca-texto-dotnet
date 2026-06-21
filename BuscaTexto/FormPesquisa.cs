using System;
using System.Drawing;
using System.Windows.Forms;

namespace BuscaTexto
{
    public partial class FormPesquisa : Form
    {
        public string TextoBusca { get; private set; }
        public string TextoSubstituicao { get; private set; }
        public bool CaseSensitive { get; private set; }
        public bool Substituir { get; private set; }

        private Label lblBusca;
        private TextBox txtBusca;
        private Label lblSubstituicao;
        private TextBox txtSubstituicao;
        private CheckBox chkCaseSensitive;
        private Button btnPesquisar;
        private Button btnSubstituir;
        private Button btnCancelar;

        public FormPesquisa(string titulo)
        {
            InitializeComponentCustom(titulo);
        }

        private void InitializeComponentCustom(string titulo)
        {
            this.Text = titulo;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(420, 200);

            // Label Busca
            lblBusca = new Label();
            lblBusca.Text = "Texto da busca:";
            lblBusca.Location = new Point(12, 15);
            lblBusca.AutoSize = true;
            this.Controls.Add(lblBusca);

            // TextBox Busca
            txtBusca = new TextBox();
            txtBusca.Location = new Point(12, 35);
            txtBusca.Size = new Size(390, 23);
            this.Controls.Add(txtBusca);

            // Label Substituição
            lblSubstituicao = new Label();
            lblSubstituicao.Text = "Substituir por:";
            lblSubstituicao.Location = new Point(12, 65);
            lblSubstituicao.AutoSize = true;
            this.Controls.Add(lblSubstituicao);

            // TextBox Substituição
            txtSubstituicao = new TextBox();
            txtSubstituicao.Location = new Point(12, 85);
            txtSubstituicao.Size = new Size(390, 23);
            this.Controls.Add(txtSubstituicao);

            // CheckBox Case Sensitive
            chkCaseSensitive = new CheckBox();
            chkCaseSensitive.Text = "Diferenciar maiúsculas de minúsculas (Case Sensitive)";
            chkCaseSensitive.Location = new Point(12, 115);
            chkCaseSensitive.AutoSize = true;
            chkCaseSensitive.Checked = true;
            this.Controls.Add(chkCaseSensitive);

            // Botão Pesquisar
            btnPesquisar = new Button();
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.Location = new Point(110, 155);
            btnPesquisar.Size = new Size(90, 30);
            btnPesquisar.Click += BtnPesquisar_Click;
            this.Controls.Add(btnPesquisar);

            // Botão Substituir
            btnSubstituir = new Button();
            btnSubstituir.Text = "Substituir";
            btnSubstituir.Location = new Point(210, 155);
            btnSubstituir.Size = new Size(90, 30);
            btnSubstituir.Click += BtnSubstituir_Click;
            this.Controls.Add(btnSubstituir);

            // Botão Cancelar
            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(310, 155);
            btnCancelar.Size = new Size(90, 30);
            btnCancelar.Click += BtnCancelar_Click;
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnPesquisar;
            this.CancelButton = btnCancelar;
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBusca.Text))
            {
                MessageBox.Show("Digite o texto da busca.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TextoBusca = txtBusca.Text;
            CaseSensitive = chkCaseSensitive.Checked;
            Substituir = false;
            TextoSubstituicao = "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnSubstituir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBusca.Text))
            {
                MessageBox.Show("Digite o texto da busca.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TextoBusca = txtBusca.Text;
            CaseSensitive = chkCaseSensitive.Checked;
            Substituir = true;
            TextoSubstituicao = txtSubstituicao.Text ?? "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
