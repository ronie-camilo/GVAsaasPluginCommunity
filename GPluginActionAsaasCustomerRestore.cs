using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerRestore : GPluginAction
    {
        //Definições de identificação e exibição da ação do plugin
        public override string ID => "C6926C40-F906-4BF9-0003-462296E7027E";
        public override string Name => "Clientes - Restaurar um cliente removido";
        public override string Description => "Restaurar um cliente removido no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerRestore(IGPlugin Plugin) : base(Plugin)
        {
            //Metodo para definição dos parametros da ação do plugin
            _Paramiters = new List<GPluginActionParameter>()
            {
                //Parametros de informações requeridas pela ação do plugin
                new GPluginActionParameter() { ID = 1, Name = "Ambiente (S=Sandbox e P=Produção)",      Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "Token Asaas (Requerido)",                Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "ID do cliente (Requerido)",              Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de retornos da ação do plugin
                new GPluginActionParameter() { ID = 4, Name = "Retorno - Estado de remoção (Checkbox)", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() { ID = 5, Name = "Retorno - Retorno da API (Textbox)",     Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            //Inicialização das variaveis para tratamento dos parametros recebidos para a ação do plugin
            string Ambiente                     = (this.Parameters[0].Value.ToString() != "" ? this.Parameters[0].Value.ToString() : "\"S\"");
            string Token                        = (this.Parameters[1].Value.ToString() != "" ? this.Parameters[1].Value.ToString() : "\"\"");
            string ClienteID                    = (this.Parameters[2].Value.ToString() != "" ? this.Parameters[2].Value.ToString() : "\"\"");

            //Inicialização das variaveis para tratamento dos retornos da ação do plugin
            IGPluginControl RetEstadoRemocao    = this.Parameters[3].Value as IGPluginControl;
            IGPluginControl Content             = this.Parameters[4].Value as IGPluginControl;

            //Variavel para controle da identação que sera usada na escrita da ação do plugin
            string indentStr = new string('\t', Identation);

            //Escreve o código durante a geração de fontes do Gvinci
            Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerRestore(Token: {Token}, CustomerID: {ClienteID}, Environment: {Ambiente});");
            Builder.AppendLine(indentStr + "if (response != null)");
            Builder.AppendLine(indentStr + "{");
            Builder.AppendLine(indentStr + '\t' + RetEstadoRemocao.Name + ".Checked = response.deleted;");
            Builder.AppendLine(indentStr + '\t' + Content.Name + ".Text = response.content;");
            Builder.AppendLine(indentStr + "}");
        }
    }
}
