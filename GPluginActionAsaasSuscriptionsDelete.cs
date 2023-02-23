using System.Collections.Generic;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasSuscriptionsDelete : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0007-462296E7027E}";

        public override string Name => "Deletar assinatura";

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
                new GPluginActionParameter() { ID = 3, Name = "Ambiente Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Estado da Cobrança/Conteudo do retorno", Type = PluginActionParameterTypeEnum.STRING },
            };
        }
        public override string GetActionCall()
        {
            string code = $"var client = GvinciAsaasCommunity.Asaas_SuscriptionsDelete({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value});";
            return code;
        }
    }
}
