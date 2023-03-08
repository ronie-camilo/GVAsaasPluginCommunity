using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerRemove : GPluginAction
    {
        //Definições de identificação e exibição da ação do plugin
        public override string ID => "{C6926C40-F906-4BF9-0002-462296E7027E}";
        public override string Name => "Clientes - Remover um cliente existente";
        public override string Description => "Remover um cliente existente no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerRemove(IGPlugin Plugin) : base(Plugin)
        {
            //Metodo para definição dos parametros da ação do plugin
            _Paramiters = new List<GPluginActionParameter>()
            {
                //Parametros de informações requeridas pela ação do plugin
                new GPluginActionParameter() { ID = 1, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "Token Asaas (Requerido)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "ID do cliente (Requerido)", Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de retornos da ação do plugin
                new GPluginActionParameter() { ID = 4, Name = "Retorno - Estado de remoção (Checkbox)", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() { ID = 5, Name = "Retorno - Retorno da API (Textbox)", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
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

            //Verifica se as informações requeridas foram informadas para prossegir
            if (Token != "" && ClienteID != "")
            {
                Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerRemove(Token: {Token}, CustomerID: {ClienteID}, Environment: {Ambiente});");

                //Testa se foi informado um controle para receber o referido dado de retorno
                if (RetEstadoRemocao.Name != "")
                {
                    Builder.AppendLine(indentStr + RetEstadoRemocao.Name + ".Checked = response.deleted;");
                }

                //Testa se foi informado um controle para receber o referido dado de retorno
                if (Content.Name != "")
                {
                    Builder.AppendLine(indentStr + Content.Name + ".Text = response.content;");
                }
            }
        }
    }
}
