using System;
using System.Collections.Generic;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasPaymentsCreate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0003-462296E7027E}";

        public override string Name => "Emitir boleto";

        public override string Description => "Emitir boleto no Asaas";

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
                new GPluginActionParameter() { ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "ID do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Vencimento da cobrança", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Valor da cobrança", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 5, Name = "Descrição da cobrança", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 6, Name = "Documento de referencia", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 7, Name = "Percentual de juros", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 8, Name = "Percentual de multa", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 9, Name = "Ambiente Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 10, Name = "ID da Cobrança/Conteudo do retorno", Type = PluginActionParameterTypeEnum.STRING },
            };
        }

        public override string GetActionCall()
        {
            string code = $"var client = GvinciAsaasCommunity.Asaas_PaymentsCreate({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value}, {this.Parameters[4].Value}, {this.Parameters[5].Value}, {this.Parameters[6].Value}, {this.Parameters[7].Value}, {this.Parameters[8].Value}, {this.Parameters[9].Value});";
            return code;
        }

    }
}