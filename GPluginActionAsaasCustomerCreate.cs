using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerCreate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0001-462296E7027E}";

        public override string Name => "Clientes - Incluir novo ou atualizar";

        public override string Description => "Incluir cliente no Asaas";

        private List<GPluginActionParameter> _Paramiters;

        public override List<GPluginActionParameter> Parameters
        {
            get
            {
                return _Paramiters;
            }
        }
        public GPluginActionAsaasCustomerCreate(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() {ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 2 , Name = "Nome do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 3, Name = "Cpf/Cnpj", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 4, Name = "Emdereço de e-mail", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 5, Name = "Telefone", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 6, Name = "Celular", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 7, Name = "Emdereço", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 8, Name = "Número", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 9, Name = "Complemento", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 10, Name = "Bairro", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 11, Name = "Cep", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 12, Name = "Referencia externa", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 13, Name = "Notificação desabilitada", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 14, Name = "Emails adicionais", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 15, Name = "Inscrição municipal", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 16, Name = "Inscrição estadual", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 17, Name = "Observações", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 18, Name = "Nome do Grupo", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 19, Name = "Retorno - ID do cliente", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] {"GTEXTBOX"} },
                new GPluginActionParameter() {ID = 20, Name = "Retorno - Conteudo do retorno da API", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] {"GTEXTBOX"} },
                new GPluginActionParameter() {ID = 21, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
           };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            string Token = (this.Parameters[0].Value.ToString() != "" ? this.Parameters[0].Value.ToString() : "\"\"");
            string Nome = (this.Parameters[1].Value.ToString() != "" ? this.Parameters[1].Value.ToString() : "\"\"");
            string CpfCnpj = (this.Parameters[2].Value.ToString() != "" ? this.Parameters[2].Value.ToString() : "\"\"");
            string Email = (this.Parameters[3].Value.ToString() != "" ? this.Parameters[3].Value.ToString() : "\"\"");
            string Telefone = (this.Parameters[4].Value.ToString() != "" ? this.Parameters[4].Value.ToString() : "\"\"");
            string Celular = (this.Parameters[5].Value.ToString() != "" ? this.Parameters[5].Value.ToString() : "\"\"");
            string Endereco = (this.Parameters[6].Value.ToString() != "" ? this.Parameters[6].Value.ToString() : "\"\"");
            string Numero = (this.Parameters[7].Value.ToString() != "" ? this.Parameters[7].Value.ToString() : "\"\"");
            string Complemento = (this.Parameters[8].Value.ToString() != "" ? this.Parameters[8].Value.ToString() : "\"\"");
            string Bairro = (this.Parameters[9].Value.ToString() != "" ? this.Parameters[9].Value.ToString() : "\"\"");
            string Cep = (this.Parameters[10].Value.ToString() != "" ? this.Parameters[10].Value.ToString() : "\"\"");
            string Referencia = (this.Parameters[11].Value.ToString() != "" ? this.Parameters[11].Value.ToString() : "\"\"");
            string NotificacaoDesabilitada = (this.Parameters[12].Value.ToString() != "" ? this.Parameters[12].Value.ToString() : "\"false\"");
            string EmailsAdicionais = (this.Parameters[13].Value.ToString() != "" ? this.Parameters[13].Value.ToString() : "\"\"");
            string InscricaoMunicipal = (this.Parameters[14].Value.ToString() != "" ? this.Parameters[14].Value.ToString() : "\"\"");
            string InscricaoEstadual = (this.Parameters[15].Value.ToString() != "" ? this.Parameters[15].Value.ToString() : "\"\"");
            string Observacoes = (this.Parameters[16].Value.ToString() != "" ? this.Parameters[16].Value.ToString() : "\"\"");
            string NomeGrupo = (this.Parameters[17].Value.ToString() != "" ? this.Parameters[17].Value.ToString() : "\"\"");

            IGPluginControl RetClienteID = this.Parameters[18].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[19].Value as IGPluginControl;

            string Ambiente = (this.Parameters[20].Value.ToString() != "" ? this.Parameters[20].Value.ToString() : "\"S\"");

            string indentStr = new string('\t', Identation);

            Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerCreate(Token: { Token }, Name: { Nome }, CpfCnpj: { CpfCnpj }, Email: { Email }, Phone: { Telefone }, Mobilephone: { Celular }, Address: { Endereco }, AddressNumber: { Numero }, Complement: { Complemento }, Province: { Bairro }, PostalCode: { Cep }, ExternalReference: { Referencia }, NotificationDisabled: bool.Parse({ NotificacaoDesabilitada }), AdditionalEmails: { EmailsAdicionais }, MunicipalInscription: { InscricaoMunicipal }, StateInscription: { InscricaoEstadual }, Observations: { Observacoes }, GroupName: { NomeGrupo }, Environment: { Ambiente });");

            if (RetClienteID.Name != "")
                Builder.AppendLine(indentStr + RetClienteID.Name + ".Text = response.id;");
            
            if (Content.Name != "")
                Builder.AppendLine(indentStr + Content.Name + ".Text = response.content;");

        }

    }

}