using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerCreateOrUpdate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0002-462296E7027E}";

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
        public GPluginActionAsaasCustomerCreateOrUpdate(IGPlugin Plugin) : base(Plugin)
        {
            _Paramiters = new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() {ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 2 , Name = "Código do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 3 , Name = "Nome do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 4, Name = "Cpf/Cnpj", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 5, Name = "Emdereço de e-mail", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 6, Name = "Telefone", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 7, Name = "Celular", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 8, Name = "Emdereço", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 9, Name = "Número", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 10, Name = "Complemento", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 11, Name = "Bairro", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 12, Name = "Cep", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 13, Name = "Referencia externa", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 14, Name = "Notificação desabilitada", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 15, Name = "Emails adicionais", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 16, Name = "Inscrição municipal", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 17, Name = "Inscrição estadual", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 18, Name = "Observações", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 19, Name = "Nome do Grupo", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() {ID = 20, Name = "Retorno - ID do cliente", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] {"GTEXTBOX"} },
                new GPluginActionParameter() {ID = 21, Name = "Retorno - Conteudo do retorno da API", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] {"GTEXTBOX"} },
                new GPluginActionParameter() {ID = 22, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
           };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            string Token = this.Parameters[0].Value.ToString();
            string ClienteID = this.Parameters[1].Value.ToString();
            string Nome = this.Parameters[2].Value.ToString();
            string CpfCnpj = this.Parameters[3].Value.ToString();
            string Email = this.Parameters[4].Value.ToString();
            string Telefone = this.Parameters[5].Value.ToString();
            string Celular = this.Parameters[6].Value.ToString();
            string Endereco = this.Parameters[7].Value.ToString();
            string Numero = this.Parameters[8].Value.ToString();
            string Complemento = this.Parameters[9].Value.ToString();
            string Bairro = this.Parameters[10].Value.ToString();
            string Cep = this.Parameters[11].Value.ToString();
            string Referencia = this.Parameters[12].Value.ToString();
            bool NotificacaoDesabilitada = bool.Parse(this.Parameters[13].Value.ToString());
            string EmailsAdicionais = this.Parameters[14].Value.ToString();
            string InscricaoMunicipal = this.Parameters[15].Value.ToString();
            string InscricaoEstadual = this.Parameters[16].Value.ToString();
            string Observacoes = this.Parameters[17].Value.ToString();
            string NomeGrupo = this.Parameters[18].Value.ToString();

            IGPluginControl RetClienteID = this.Parameters[19].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[20].Value as IGPluginControl;

            string Ambiente = this.Parameters[21].Value.ToString();

            string indentStr = new string('\t', Identation);

            Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerCreateOrUpdate(Token: { Token }, CustomerID: { ClienteID }, Name: { Nome }, CpfCnpj: { CpfCnpj }, Email: { Email }, Phone: { Telefone }, Mobilephone: { Celular }, Address: { Endereco }, AddressNumber: { Numero }, Complement: { Complemento }, Province: { Bairro }, PostalCode: { Cep }, ExternalReference: { Referencia }, NotificationDisabled: { NotificacaoDesabilitada }, AdditionalEmails: { EmailsAdicionais }, MunicipalInscription: { InscricaoMunicipal }, StateInscription: { InscricaoEstadual }, Observations: { Observacoes }, GroupName: { NomeGrupo }, Environment: { Ambiente });");

            Builder.AppendLine(indentStr + RetClienteID.Name + ".Text = response.id");
            Builder.AppendLine(indentStr + Content.Name + ".Text = response.content");
        }

    }

}