using System.Collections.Generic;
using System.Text;

namespace Gvinci.Plugin.Action
{
    internal class GPluginActionAsaasCustomerCreate : GPluginAction
    {
        public override string ID => "{C6926C40-F906-4BF9-0001-462296E7027E}";

        public override string Name => "Cadastrar cliente";

        public override string Description => "Cadastrar cliente no Asaas";

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
            _Paramiters= new List<GPluginActionParameter>()
            {
                new GPluginActionParameter() { ID = 1, Name = "Token Asaas", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 2, Name = "Nome do cliente", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 3, Name = "Cpf/Cnpj", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 4, Name = "Emdereço de e-mail", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 5, Name = "Telefone", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 6, Name = "Celular", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 7, Name = "Cep", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 8, Name = "Número", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 9, Name = "Referencia externa", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 10, Name = "Ambiente (S=Sandbox e P=Produção)", Type = PluginActionParameterTypeEnum.STRING },
                new GPluginActionParameter() { ID = 11, Name = "Retorno - ID do cliente", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
                new GPluginActionParameter() { ID = 12, Name = "Retorno - Conteudo do retorno da API", Type = PluginActionParameterTypeEnum.CONTROL, AllowedControlTypes = new string[] { "GTEXTBOX" } },
            };
        }

        public override void WriteActionCall(StringBuilder Builder, int Identation, int ActionSequence)
        {
            IGPluginControl CustomerID = this.Parameters[11].Value as IGPluginControl;
            IGPluginControl Content = this.Parameters[12].Value as IGPluginControl;

            string indentStr = new string('\t', Identation);
            Builder.AppendLine(indentStr + $"var response = GvinciAsaasCommunity.Asaas_CustomerCreateOrUpdate({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value}, {this.Parameters[4].Value}, {this.Parameters[5].Value}, {this.Parameters[6].Value}, {this.Parameters[7].Value}, {this.Parameters[8].Value}, {this.Parameters[9].Value});");
            Builder.AppendLine(indentStr + CustomerID.Name + ".Text = response.id");
            Builder.AppendLine(indentStr + Content.Name + ".Text = response.content");
        }

        //public override string GetActionCall()
        //{
        //    IGPluginControl CustomerID = this.Parameters[11].Value as IGPluginControl;
        //    IGPluginControl Content = this.Parameters[12].Value as IGPluginControl;

        //    string indentStr = new string('\t', Identation);
        //    string code = $"var client = GvinciAsaasCommunity.Asaas_CustomerCreateOrUpdate({this.Parameters[0].Value}, {this.Parameters[1].Value}, {this.Parameters[2].Value}, {this.Parameters[3].Value}, {this.Parameters[4].Value}, {this.Parameters[5].Value}, {this.Parameters[6].Value}, {this.Parameters[7].Value}, {this.Parameters[8].Value}, {this.Parameters[9].Value}, {this.Parameters[10].Value});";
        //    return code;
        //}

    }
}