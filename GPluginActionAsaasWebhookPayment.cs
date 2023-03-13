using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasWebhookPayment : GPluginAction
    {
        public override string ID => "C6926C40-F906-4BF9-0010-462296E7027E";

        public override string Name => "Webhook - Receber aviso de cobraça via Webhook";

        public override string Description => "Receber aviso de cobraça via Webhook no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasWebhookPayment(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() { ID = 1, Name = "Token Asaas (Requerido)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "ID do cliente (Requerido)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Retorno - Estado de remoção (Checkbox)", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() { ID = 5, Name = "Retorno - Retorno da API (Textbox)", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            string Token = (this.Parameters[0].Value.ToString() != "" ? this.Parameters[0].Value.ToString() : "\"\"");
            string ClienteID = (this.Parameters[1].Value.ToString() != "" ? this.Parameters[1].Value.ToString() : "\"\"");

            IGPluginControl RetEstadoRemocao = this.Parameters[3].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[4].Value as IGPluginControl;

            string Ambiente = (this.Parameters[2].Value.ToString() != "" ? this.Parameters[2].Value.ToString() : "\"S\"");

            string indentStr = new string('\t', Identation);

            if (Token != "" && ClienteID != "")
            {
                Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerRestore(Token: {Token}, CustomerID: {ClienteID}, Environment: {Ambiente});");

                if (RetEstadoRemocao.Name != "")
                {
                    Builder.AppendLine(indentStr + RetEstadoRemocao.Name + ".Checked = response.deleted;");
                }

                if (Content.Name != "")
                {
                    Builder.AppendLine(indentStr + Content.Name + ".Text = response.content;");
                }

            }

        }

    }

}