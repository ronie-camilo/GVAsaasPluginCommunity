using System;
using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasSuscriptionsCreate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0006-462296E7027E}";

        public override string Name => "Emitir assinatura";

        public override string Description => "Emitir assinatura no Asaas";

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
                new GPluginActionParameter() { ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "ID do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Forma de pagamento", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Vencimento da próxima mensalidade", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 5, Name = "Valor da assinatura", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 6, Name = "Periodicidade da cobrança", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 7, Name = "Descrição da assinatura", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 8, Name = "Valor de desconto", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 9, Name = "Dias antes do vencimento para aplicar desconto", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 10, Name = "Percentual de multa", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 11, Name = "Percentual de juros", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 12, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 13, Name = "Retorno - ID da cobrança", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() { ID = 14, Name = "Retorno - Conteudo do retorno da API", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            IGPluginControl PaymentsID = this.Parameters[13].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[14].Value as IGPluginControl;

            string indentStr = new string('\t', Identation);
            Builder.AppendLine(indentStr + $"var client = GvinciAsaasCommunity.SuscriptionsCreate({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value}, {this.Parameters[4].Value}, {this.Parameters[5].Value}, {this.Parameters[6].Value}, {this.Parameters[7].Value}, {this.Parameters[8].Value}, {this.Parameters[9].Value}, {this.Parameters[10].Value}, {this.Parameters[11].Value});");
            Builder.AppendLine(indentStr + PaymentsID.Name + ".Text = response.id");
            Builder.AppendLine(indentStr + Content.Name + ".Text = response.content");
        }

    }

}
