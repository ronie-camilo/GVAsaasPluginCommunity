using System.Collections.Generic;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasSuscriptionsCreate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0006-462296E7027E}";

        public override string Name => "Cadastra assinatura";

        public override string Description => "";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasSuscriptionsCreate(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() { ID = 1, Name = "Token", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "PaymentsID", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Ambiente Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "SuscriptionsID/Content", Type = PluginActionParameterTypeEnum.STRING },
            };
        }
        public override string GetActionCall()
        {
            string code = $"var client = GvinciAsaasCommunity.SuscriptionsCreate({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value});";
            return code;
        }
    }
}
