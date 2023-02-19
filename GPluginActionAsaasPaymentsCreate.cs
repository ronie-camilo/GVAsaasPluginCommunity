using System;
using System.Collections.Generic;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasPaymentsCreate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0003-462296E7027E}";

        public override string Name => "Cadastra boleto";

        public override string Description => "";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasPaymentsCreate(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() { ID = 1, Name = "Token", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "CustomerID", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "DueDate", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Value", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 5, Name = "Description", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 6, Name = "DocRef", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 7, Name = "Interest Value", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 8, Name = "Fine Value", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 9, Name = "Ambiente Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 10, Name = "PaimentsID/Content", Type = PluginActionParameterTypeEnum.STRING },
            };
        }

        public override string GetActionCall()
        {
            string code = $"var client = GPluginAssasCommunity.Asaas_PaymentsCreate({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value}, {this.Parameters[4].Value}, {this.Parameters[5].Value}, {this.Parameters[6].Value}, {this.Parameters[7].Value}, {this.Parameters[8].Value}, {this.Parameters[9].Value});";
            return code;
        }

    }
}