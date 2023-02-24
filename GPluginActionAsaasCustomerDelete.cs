using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerDelete : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0002-462296E7027E}";

        public override string Name => "Deletar cliente";

        public override string Description => "Deletar cliente no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerDelete(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() { ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "ID do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Ambiente Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 12, Name = "Retorno - Estado de deleção do cliente", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() { ID = 12, Name = "Retorno - Conteudo do retorno da API", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            IGPluginControl CustomerState = this.Parameters[12].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[12].Value as IGPluginControl;

            string indentStr = new string('\t', Identation);
            Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerDelete({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value});");
            Builder.AppendLine(indentStr + CustomerState.Name + ".Text = response.deleted");
            Builder.AppendLine(indentStr + Content.Name + ".Text = response.content");
        }

    }

}
