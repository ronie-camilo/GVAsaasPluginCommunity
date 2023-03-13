using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasSuscriptionsDelete : GPluginAction
    {
        public override string ID => "C6926C40-F906-4BF9-0009-462296E7027E";

        public override string Name => "Assinaturas - Apagar";

        public override string Description => "Deletar assinatura no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }

        public GPluginActionAsaasSuscriptionsDelete(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() { ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "ID da Cobrança", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Retorno - Estado da deleção da cobrança", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() { ID = 5, Name = "Retorno - Conteudo do retorno da API", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            IGPluginControl SuscriptionsState = this.Parameters[4].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[5].Value as IGPluginControl;

            string indentStr = new string('\t', Identation);
            Builder.AppendLine(indentStr + $"var client = GvinciAsaasCommunity.Asaas_SuscriptionsDelete({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value});");
            Builder.AppendLine(indentStr + SuscriptionsState.Name + ".Text = response.deleted");
            Builder.AppendLine(indentStr + Content.Name + ".Text = response.content");
        }

    }

}
