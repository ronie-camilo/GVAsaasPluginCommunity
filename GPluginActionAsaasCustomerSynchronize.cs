using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerSynchronize : GPluginAction
    {
        //Definições de identificação e exibição da ação do plugin
        public override string ID => "C6926C40-F906-4BF9-0001-462296E7027E";
        public override string Name => "Clientes - Incluir cliente novo ou atualizar existente";
        public override string Description => "";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerSynchronize(IGPlugin Plugin) : base(Plugin)
        {
            //Metodo para definição dos parametros da ação do plugin
            _Paramiters = new List<GPluginActionParameter>()
            {
                //Parametros de informações requeridas pela ação do plugin
                new GPluginActionParameter() { ID =  1, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  2, Name = "Token Asaas (Requerido)",           Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  3, Name = "Código do cliente",                 Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  4, Name = "Nome do cliente (Requirido)",       Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  5, Name = "Cpf/Cnpj (Requerido)",              Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de informações adicionais na ação do plugin
                new GPluginActionParameter() { ID =  6, Name = "Emdereço de e-mail",                Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  7, Name = "Telefone",                          Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  8, Name = "Celular",                           Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID =  9, Name = "Emdereço",                          Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 10, Name = "Número",                            Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 11, Name = "Complemento",                       Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 12, Name = "Bairro",                            Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 13, Name = "Cep",                               Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 14, Name = "Referencia externa",                Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 15, Name = "Notificação desabilitada",          Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 16, Name = "Emails adicionais",                 Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 17, Name = "Inscrição municipal",               Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 18, Name = "Inscrição estadual",                Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 19, Name = "Observações",                       Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 20, Name = "Nome do Grupo",                     Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de retornos da ação do plugin
                new GPluginActionParameter() { ID = 21, Name = "Retorno - ID do cliente (Textbox)",  Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] {"GTEXTBOX"} },
                new GPluginActionParameter() { ID = 22, Name = "Retorno - Retorno da API (Textbox)", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] {"GTEXTBOX"} },
           };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            //Inicialização das variaveis para tratamento dos parametros recebidos para a ação do plugin
            string Ambiente                 = (this.Parameters[ 0].Value.ToString() != "" ? this.Parameters[ 0].Value.ToString() : "\"S\"");
            string Token                    = (this.Parameters[ 1].Value.ToString() != "" ? this.Parameters[ 1].Value.ToString() : "\"\"");
            string ClienteID                = (this.Parameters[ 2].Value.ToString() != "" ? this.Parameters[ 2].Value.ToString() : "\"\"");
            string Nome                     = (this.Parameters[ 3].Value.ToString() != "" ? this.Parameters[ 3].Value.ToString() : "\"\"");
            string CpfCnpj                  = (this.Parameters[ 4].Value.ToString() != "" ? this.Parameters[ 4].Value.ToString() : "\"\"");
            string Email                    = (this.Parameters[ 5].Value.ToString() != "" ? this.Parameters[ 5].Value.ToString() : "\"\"");
            string Telefone                 = (this.Parameters[ 6].Value.ToString() != "" ? this.Parameters[ 6].Value.ToString() : "\"\"");
            string Celular                  = (this.Parameters[ 7].Value.ToString() != "" ? this.Parameters[ 7].Value.ToString() : "\"\"");
            string Endereco                 = (this.Parameters[ 8].Value.ToString() != "" ? this.Parameters[ 8].Value.ToString() : "\"\"");
            string Numero                   = (this.Parameters[ 9].Value.ToString() != "" ? this.Parameters[ 9].Value.ToString() : "\"\"");
            string Complemento              = (this.Parameters[10].Value.ToString() != "" ? this.Parameters[10].Value.ToString() : "\"\"");
            string Bairro                   = (this.Parameters[11].Value.ToString() != "" ? this.Parameters[11].Value.ToString() : "\"\"");
            string Cep                      = (this.Parameters[12].Value.ToString() != "" ? this.Parameters[12].Value.ToString() : "\"\"");
            string ReferenciaExterna        = (this.Parameters[13].Value.ToString() != "" ? this.Parameters[13].Value.ToString() : "\"\"");
            string NotificacaoDesabilitada  = (this.Parameters[14].Value.ToString() != "" ? this.Parameters[14].Value.ToString() : "\"false\"");
            string EmailsAdicionais         = (this.Parameters[15].Value.ToString() != "" ? this.Parameters[15].Value.ToString() : "\"\"");
            string InscricaoMunicipal       = (this.Parameters[16].Value.ToString() != "" ? this.Parameters[16].Value.ToString() : "\"\"");
            string InscricaoEstadual        = (this.Parameters[17].Value.ToString() != "" ? this.Parameters[17].Value.ToString() : "\"\"");
            string Observacoes              = (this.Parameters[18].Value.ToString() != "" ? this.Parameters[18].Value.ToString() : "\"\"");
            string NomeGrupo                = (this.Parameters[19].Value.ToString() != "" ? this.Parameters[19].Value.ToString() : "\"\"");

            //Inicialização das variaveis para tratamento dos retornos da ação do plugin
            IGPluginControl RetClienteID    = this.Parameters[20].Value as IGPluginControl;
            IGPluginControl Content         = this.Parameters[21].Value as IGPluginControl;

            //Variavel para controle da identação que sera usada na escrita da ação do plugin
            string indentStr = new string('\t', Identation);

            //Verifica se as informações requeridas foram informadas para prossegir
            if (Token != "" && Nome != "" && CpfCnpj != "")
            {
                Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerSynchronize(Environment: {Ambiente}, Token: { Token }, CustomerID: {ClienteID}, Name: { Nome }, CpfCnpj: { CpfCnpj }, Email: { Email }, Phone: { Telefone }, Mobilephone: { Celular }, Address: { Endereco }, AddressNumber: { Numero }, Complement: { Complemento }, Province: { Bairro }, PostalCode: { Cep }, ExternalReference: { ReferenciaExterna }, NotificationDisabled: bool.Parse({ NotificacaoDesabilitada }), AdditionalEmails: { EmailsAdicionais }, MunicipalInscription: { InscricaoMunicipal }, StateInscription: { InscricaoEstadual }, Observations: { Observacoes }, GroupName: { NomeGrupo });");

                //Testa se foi informado um controle para receber o referido dado de retorno
                if (RetClienteID.Name != "")
                {
                    Builder.AppendLine(indentStr + RetClienteID.Name + ".Text = response.id;");
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