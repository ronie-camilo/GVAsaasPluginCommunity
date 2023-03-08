﻿using System.Collections.Generic;
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
                new GPluginActionParameter() {ID =  9, Name = "Posição inicial da lista",                       Type = PluginActionParameterTypeEnum.INTEGER },
                new GPluginActionParameter() {ID = 10, Name = "Tamanho da lista",                               Type = PluginActionParameterTypeEnum.INTEGER },

                //Parametros de retornos da ação do plugin
                new GPluginActionParameter() {ID = 11, Name = "Retorno - Código do cliente (Textbox)",          Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 12, Name = "Retorno - Data de criação (Textbox)",            Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 13, Name = "Retorno - Nome do cliente (Textbox)",            Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 14, Name = "Retorno - Emdereço de e-mail (Textbox)",         Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 15, Name = "Retorno - Telefone (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 16, Name = "Retorno - Celular (Textbox)",                    Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 17, Name = "Retorno - Emdereço (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 18, Name = "Retorno - Número (Textbox)",                     Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 19, Name = "Retorno - Complemento (Textbox)",                Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 20, Name = "Retorno - Bairro (Textbox)",                     Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 21, Name = "Retorno - Cep (Textbox)",                        Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 12, Name = "Retorno - Cpf/Cnpj (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 23, Name = "Retorno - Tipo de pessoa (Textbox)",             Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 24, Name = "Retorno - Removido (Textbox)",                   Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 25, Name = "Retorno - Emails adicionais (Textbox)",          Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 26, Name = "Retorno - Referencia externa (Textbox)",         Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 27, Name = "Retorno - Desabilitar notificações (Checkbox)",  Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 28, Name = "Retorno - Inscrição municipal (Textbox)",        Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 29, Name = "Retorno - Inscrição estadual (Textbox)",         Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 30, Name = "Retorno - Observações (Textbox)",                Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 31, Name = "Retorno - Nome do Grupo (Textbox)",              Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() {ID = 32, Name = "Retorno - Retorno da API (Textbox)",             Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            string Token = this.Parameters[0].Value.ToString();
            string ClienteID = this.Parameters[1].Value.ToString();
            string Nome = this.Parameters[2].Value.ToString();
            string CpfCnpj = this.Parameters[3].Value.ToString();
            string Email = this.Parameters[4].Value.ToString();
            string NomeGrupo = this.Parameters[5].Value.ToString();
            string ReferenciaExterna = this.Parameters[6].Value.ToString();
            int OffSet = int.Parse(this.Parameters[7].Value.ToString());
            int Limite = int.Parse(this.Parameters[8].Value.ToString());

            IGPluginControl RetClienteID = this.Parameters[9].Value as IGPluginControl;
            IGPluginControl RetNome = this.Parameters[10].Value as IGPluginControl;
            IGPluginControl RetCpfCnpj = this.Parameters[11].Value as IGPluginControl;
            IGPluginControl RetEmail = this.Parameters[12].Value as IGPluginControl;
            IGPluginControl RetTelefone = this.Parameters[13].Value as IGPluginControl;
            IGPluginControl RetCelular = this.Parameters[14].Value as IGPluginControl;
            IGPluginControl RetEndereco = this.Parameters[15].Value as IGPluginControl;
            IGPluginControl RetNumero = this.Parameters[16].Value as IGPluginControl;
            IGPluginControl RetComplemento = this.Parameters[17].Value as IGPluginControl;
            IGPluginControl RetBairro = this.Parameters[18].Value as IGPluginControl;
            IGPluginControl RetCep = this.Parameters[19].Value as IGPluginControl;
            IGPluginControl RetReferenciaExterna = this.Parameters[20].Value as IGPluginControl;
            IGPluginControl RetNotificacaoDesabilitada = this.Parameters[21].Value as IGPluginControl;
            IGPluginControl RetEmailsAdicionais = this.Parameters[22].Value as IGPluginControl;
            IGPluginControl RetInscricaoMunicipal = this.Parameters[23].Value as IGPluginControl;
            IGPluginControl RetInscricaoEstadual = this.Parameters[24].Value as IGPluginControl;
            IGPluginControl RetObservacoes = this.Parameters[25].Value as IGPluginControl;
            IGPluginControl RetNomeGrupo = this.Parameters[26].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[27].Value as IGPluginControl;

            string Ambiente = this.Parameters[28].Value.ToString();

            string indentStr = new string('\t', Identation);

            Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerSearch(Token: { Token }, CustomerID: { ClienteID}, Name: { Nome }, CpfCnpj: { CpfCnpj }, Email: { Email }, GroupName: { NomeGrupo }, ExternalReference: { ReferenciaExterna }, OffSet: { OffSet }, Limit: { Limite }, Environment: { Ambiente });");

            Builder.AppendLine(indentStr + RetClienteID.Name + ".Text = listCustomers.data[0].id.ToString();");
            Builder.AppendLine(indentStr + RetNome.Name + ".Text = listCustomers.data[0].name.ToString();");
            Builder.AppendLine(indentStr + RetCpfCnpj.Name + ".Text = listCustomers.data[0].cpfCnpj.ToString();");
            Builder.AppendLine(indentStr + RetEmail.Name + ".Text = listCustomers.data[0].email.ToString();");
            Builder.AppendLine(indentStr + RetTelefone.Name + ".Text = listCustomers.data[0].phone.ToString();");
            Builder.AppendLine(indentStr + RetCelular.Name + ".Text = listCustomers.data[0].mobilePhone.ToString();");
            Builder.AppendLine(indentStr + RetEndereco.Name + ".Text = listCustomers.data[0].address.ToString();");
            Builder.AppendLine(indentStr + RetNumero.Name + ".Text = listCustomers.data[0].addressNumber.ToString();");
            Builder.AppendLine(indentStr + RetComplemento.Name + ".Text = listCustomers.data[0].complement.ToString();");
            Builder.AppendLine(indentStr + RetBairro.Name + ".Text = listCustomers.data[0].province.ToString();");
            Builder.AppendLine(indentStr + RetCep.Name + ".Text = listCustomers.data[0].postalCode.ToString();");
            Builder.AppendLine(indentStr + RetReferenciaExterna.Name + ".Text = listCustomers.data[0].externalReference.ToString();");
            Builder.AppendLine(indentStr + RetNotificacaoDesabilitada.Name + ".Text = listCustomers.data[0].notificationDisabled.ToString();");
            Builder.AppendLine(indentStr + RetEmailsAdicionais.Name + ".Text = listCustomers.data[0].additionalEmails.ToString();");
            Builder.AppendLine(indentStr + RetInscricaoMunicipal.Name + ".Text = listCustomers.data[0].municipalInscription.ToString();");
            Builder.AppendLine(indentStr + RetInscricaoEstadual.Name + ".Text = listCustomers.data[0].stateInscription.ToString();");
            Builder.AppendLine(indentStr + RetObservacoes.Name + ".Text = listCustomers.data[0].observations.ToString();");
            Builder.AppendLine(indentStr + RetNomeGrupo.Name + ".Text = listCustomers.data[0].groups[0].name.ToString();");
            Builder.AppendLine(indentStr + Content.Name + ".Text = listCustomers.data[0].content.ToString();");
        }

    }

}
