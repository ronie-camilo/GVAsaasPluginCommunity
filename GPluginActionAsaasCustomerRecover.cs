using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerRecover : GPluginAction
    {
        //Definições de identificação e exibição da ação do plugin
        public override string ID => "C6926C40-F906-4BF9-0004-462296E7027E";
        public override string Name => "Clientes - Recuperar informações de cliente existente";
        public override string Description => "";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerRecover(IGPlugin Plugin) : base(Plugin)
        {
            //Metodo para definição dos parametros da ação do plugin
            _Paramiters = new List<GPluginActionParameter>()
            {
                //Parametros de informações requeridas pela ação do plugin
                new GPluginActionParameter() { ID =  1, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  2, Name = "Token Asaas (Requerido)",           Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  3, Name = "Cpf/Cnpj (Requerido)",              Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de informações adicionais na ação do plugin
                new GPluginActionParameter() { ID =  4, Name = "Código do cliente",                 Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  5, Name = "Nome do cliente",                   Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  6, Name = "Emdereço de e-mail",                Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  7, Name = "Nome do Grupo",                     Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  8, Name = "Referencia externa",                Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de retornos da ação do plugin
                new GPluginActionParameter() { ID =  9, Name = "Retorno - Código do cliente (Textbox)",          Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() { ID = 10, Name = "Retorno - Removido (Checkbox)",                  Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() { ID = 11, Name = "Retorno - Desabilitar notificações (Checkbox)",  Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() { ID = 12, Name = "Retorno - Retorno da API (Textbox)",             Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            //Inicialização das variaveis para tratamento dos parametros recebidos para a ação do plugin
            string Ambiente                             = (this.Parameters[ 0].Value.ToString() != "" ? this.Parameters[ 0].Value.ToString() : "\"S\"");
            string Token                                = (this.Parameters[ 1].Value.ToString() != "" ? this.Parameters[ 1].Value.ToString() : "\"\"");
            string CpfCnpj                              = (this.Parameters[ 2].Value.ToString() != "" ? this.Parameters[ 2].Value.ToString() : "\"\"");
            string ClienteID                            = (this.Parameters[ 3].Value.ToString() != "" ? this.Parameters[ 3].Value.ToString() : "\"\"");
            string Nome                                 = (this.Parameters[ 4].Value.ToString() != "" ? this.Parameters[ 4].Value.ToString() : "\"\"");
            string Email                                = (this.Parameters[ 5].Value.ToString() != "" ? this.Parameters[ 5].Value.ToString() : "\"\"");
            string NomeGrupo                            = (this.Parameters[ 6].Value.ToString() != "" ? this.Parameters[ 6].Value.ToString() : "\"\"");
            string ReferenciaExterna                    = (this.Parameters[ 7].Value.ToString() != "" ? this.Parameters[ 7].Value.ToString() : "\"\"");

            //Inicialização das variaveis para tratamento dos retornos da ação do plugin
            IGPluginControl RetClienteID                = this.Parameters[ 8].Value as IGPluginControl;
            IGPluginControl RetRemovido                 = this.Parameters[ 9].Value as IGPluginControl;
            IGPluginControl RetNotificacaoDesabilitada  = this.Parameters[10].Value as IGPluginControl;
            IGPluginControl Content                     = this.Parameters[11].Value as IGPluginControl;

            //Variavel para controle da identação que sera usada na escrita da ação do plugin
            string indentStr = new string('\t', Identation);

            //Verifica se as informações requeridas foram informadas para prossegir
            if (Token != "" && (CpfCnpj != "" || ClienteID != ""))
            {
                Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerRecover(Token: {Token}, CustomerID: {ClienteID}, Name: {Nome}, CpfCnpj: {CpfCnpj}, Email: {Email}, GroupName: {NomeGrupo}, ExternalReference: {ReferenciaExterna}, Environment: {Ambiente});");

                //Testa se foi informado um controle para receber o referido dado de retorno
                if (RetClienteID.Name != "")
                {
                    Builder.AppendLine(indentStr + RetClienteID.Name + ".Text = response.data[0].id;");
                }

                //Testa se foi informado um controle para receber o referido dado de retorno
                if (RetRemovido.Name != "")
                {
                    Builder.AppendLine(indentStr + RetRemovido.Name + ".Checked = response.data[0].deleted;");
                }

                //Testa se foi informado um controle para receber o referido dado de retorno
                if (RetNotificacaoDesabilitada.Name != "")
                {
                    Builder.AppendLine(indentStr + RetNotificacaoDesabilitada.Name + ".Checked = response.data[0].notificationDisabled;");
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
