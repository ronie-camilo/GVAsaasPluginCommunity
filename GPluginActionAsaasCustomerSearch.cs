using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerSearch : GPluginAction
    {
        //Definições de identificação e exibição da ação do plugin
        public override string ID => "{C6926C40-F906-4BF9-0004-462296E7027E}";
        public override string Name => "Clientes - Procurar";
        public override string Description => "Deletar cliente no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerSearch(IGPlugin Plugin) : base(Plugin)
        {
            //Metodo para definição dos parametros da ação do plugin
            _Paramiters = new List<GPluginActionParameter>()
            {
                //Parametros de informações requeridas pela ação do plugin
                new GPluginActionParameter() {ID =  1, Name = "Ambiente (S=Sandbox e P=Produção)",              Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID =  2, Name = "Token Asaas (Requerido)",                        Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID =  3, Name = "Cpf/Cnpj (Requerido)",                           Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de informações adicionais na ação do plugin
                new GPluginActionParameter() {ID =  4, Name = "Código do cliente",                              Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID =  5, Name = "Nome do cliente",                                Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID =  6, Name = "Emdereço de e-mail",                             Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID =  7, Name = "Nome do Grupo",                                  Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID =  8, Name = "Referencia externa",                             Type = PluginActionParameterTypeEnum.STRING },

                //Parametros de retornos da ação do plugin
                new GPluginActionParameter() {ID =  9, Name = "Retorno - Código do cliente (Textbox)",          Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 10, Name = "Retorno - Data de criação (Textbox)",            Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 11, Name = "Retorno - Nome do cliente (Textbox)",            Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 12, Name = "Retorno - Emdereço de e-mail (Textbox)",         Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 13, Name = "Retorno - Telefone (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 14, Name = "Retorno - Celular (Textbox)",                    Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 15, Name = "Retorno - Emdereço (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 16, Name = "Retorno - Número (Textbox)",                     Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 17, Name = "Retorno - Complemento (Textbox)",                Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 18, Name = "Retorno - Bairro (Textbox)",                     Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 19, Name = "Retorno - Cep (Textbox)",                        Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 20, Name = "Retorno - Cpf/Cnpj (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 21, Name = "Retorno - Tipo de pessoa (Textbox)",             Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
//                new GPluginActionParameter() {ID = 22, Name = "Retorno - Removido (Checkbox)",                  Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() {ID = 23, Name = "Retorno - Emails adicionais (Textbox)",          Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 24, Name = "Retorno - Referencia externa (Textbox)",         Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
//                new GPluginActionParameter() {ID = 25, Name = "Retorno - Desabilitar notificações (Checkbox)",  Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GCHECKBOX" } },
                new GPluginActionParameter() {ID = 26, Name = "Retorno - Inscrição municipal (Textbox)",        Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 27, Name = "Retorno - Inscrição estadual (Textbox)",         Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 28, Name = "Retorno - Observações (Textbox)",                Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 29, Name = "Retorno - Nome do Grupo (Textbox)",              Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 30, Name = "Retorno - Retorno da API (Textbox)",             Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            //Inicialização das variaveis para tratamento dos parametros recebidos para a ação do plugin
            //string Ambiente                             = (this.Parameters[ 0].Value.ToString() != "" ? this.Parameters[ 0].Value.ToString() : "\"S\"");
            //string Token                                = (this.Parameters[ 1].Value.ToString() != "" ? this.Parameters[ 1].Value.ToString() : "\"\"");
            //string CpfCnpj                              = (this.Parameters[ 2].Value.ToString() != "" ? this.Parameters[ 2].Value.ToString() : "\"\"");
            //string ClienteID                            = (this.Parameters[ 3].Value.ToString() != "" ? this.Parameters[ 3].Value.ToString() : "\"\"");
            //string Nome                                 = (this.Parameters[ 4].Value.ToString() != "" ? this.Parameters[ 4].Value.ToString() : "\"\"");
            //string Email                                = (this.Parameters[ 5].Value.ToString() != "" ? this.Parameters[ 5].Value.ToString() : "\"\"");
            //string NomeGrupo                            = (this.Parameters[ 6].Value.ToString() != "" ? this.Parameters[ 6].Value.ToString() : "\"\"");
            //string ReferenciaExterna                    = (this.Parameters[ 7].Value.ToString() != "" ? this.Parameters[ 7].Value.ToString() : "\"\"");

            //Inicialização das variaveis para tratamento dos retornos da ação do plugin
            //IGPluginControl RetClienteID                = this.Parameters[ 8].Value as IGPluginControl;
            //IGPluginControl RetDataCriacao              = this.Parameters[ 9].Value as IGPluginControl;
            //IGPluginControl RetNome                     = this.Parameters[10].Value as IGPluginControl;
            //IGPluginControl RetEmail                    = this.Parameters[11].Value as IGPluginControl;
            //IGPluginControl RetTelefone                 = this.Parameters[12].Value as IGPluginControl;
            //IGPluginControl RetCelular                  = this.Parameters[13].Value as IGPluginControl;
            //IGPluginControl RetEndereco                 = this.Parameters[14].Value as IGPluginControl;
            //IGPluginControl RetNumero                   = this.Parameters[15].Value as IGPluginControl;
            //IGPluginControl RetComplemento              = this.Parameters[16].Value as IGPluginControl;
            //IGPluginControl RetBairro                   = this.Parameters[17].Value as IGPluginControl;
            //IGPluginControl RetCep                      = this.Parameters[18].Value as IGPluginControl;
            //IGPluginControl RetCpfCnpj                  = this.Parameters[19].Value as IGPluginControl;
            //IGPluginControl RetTipoPessoa               = this.Parameters[20].Value as IGPluginControl;
            //IGPluginControl RetRemovido                 = this.Parameters[21].Value as IGPluginControl;
            //IGPluginControl RetEmailsAdicionais         = this.Parameters[22].Value as IGPluginControl;
            //IGPluginControl RetReferenciaExterna        = this.Parameters[23].Value as IGPluginControl;
            //IGPluginControl RetNotificacaoDesabilitada  = this.Parameters[24].Value as IGPluginControl;
            //IGPluginControl RetInscricaoMunicipal       = this.Parameters[25].Value as IGPluginControl;
            //IGPluginControl RetInscricaoEstadual        = this.Parameters[26].Value as IGPluginControl;
            //IGPluginControl RetObservacoes              = this.Parameters[27].Value as IGPluginControl;
            //IGPluginControl RetNomeGrupo                = this.Parameters[28].Value as IGPluginControl;
            //IGPluginControl Content                     = this.Parameters[29].Value as IGPluginControl;

            //Variavel para controle da identação que sera usada na escrita da ação do plugin
            //string indentStr = new string('\t', Identation);

            //Verifica se as informações requeridas foram informadas para prossegir
            //if (Token != "" && (CpfCnpj != "" || ClienteID != ""))
            //{
                //Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerSearch(Token: {Token}, CustomerID: {ClienteID}, Name: {Nome}, CpfCnpj: {CpfCnpj}, Email: {Email}, GroupName: {NomeGrupo}, ExternalReference: {ReferenciaExterna}, OffSet: {OffSet}, Limit: {Limite}, Environment: {Ambiente});");
                //Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerSearch(Token: {Token}, CustomerID: {ClienteID}, Name: {Nome}, CpfCnpj: {CpfCnpj}, Email: {Email}, GroupName: {NomeGrupo}, ExternalReference: {ReferenciaExterna}, Environment: {Ambiente});");

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetClienteID.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetClienteID.Name + ".Text = response.data[0].id;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetDataCriacao.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetDataCriacao.Name + ".Text = response.data[0].dateCreated;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetNome.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetNome.Name + ".Text = response.data[0].name;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetEmail.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetEmail.Name + ".Text = response.data[0].email;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetTelefone.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetTelefone.Name + ".Text = response.data[0].phone;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetCelular.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetCelular.Name + ".Text = response.data[0].mobilePhone;");
                //}
                
                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetEndereco.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetEndereco.Name + ".Text = response.data[0].address;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetNumero.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetNumero.Name + ".Text = response.data[0].addressNumber;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetComplemento.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetComplemento.Name + ".Text = response.data[0].complement;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetBairro.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetBairro.Name + ".Text = response.data[0].province;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetCep.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetCep.Name + ".Text = response.data[0].postalCode;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetCpfCnpj.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetCpfCnpj.Name + ".Text = response.data[0].cpfCnpj;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetTipoPessoa.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetTipoPessoa.Name + ".Text = response.data[0].personType;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetRemovido.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetRemovido.Name + ".Checked = response.data[0].deleted;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetReferenciaExterna.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetReferenciaExterna.Name + ".Text = response.data[0].externalReference;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetNotificacaoDesabilitada.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetNotificacaoDesabilitada.Name + ".Checked = response.data[0].notificationDisabled;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetEmailsAdicionais.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetEmailsAdicionais.Name + ".Text = response.data[0].additionalEmails;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetInscricaoMunicipal.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetInscricaoMunicipal.Name + ".Text = response.data[0].municipalInscription;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetInscricaoEstadual.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetInscricaoEstadual.Name + ".Text = response.data[0].stateInscription;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetObservacoes.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetObservacoes.Name + ".Text = response.data[0].observations;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (RetNomeGrupo.Name != "")
                //{
                //    Builder.AppendLine(indentStr + RetNomeGrupo.Name + ".Text = response.data[0].groups[0].name;");
                //}

                ////Testa se foi informado um controle para receber o referido dado de retorno
                //if (Content.Name != "")
                //{
                //    Builder.AppendLine(indentStr + Content.Name + ".Text = response.data[0].content;");
                //}
            //}
        }
    }
}
