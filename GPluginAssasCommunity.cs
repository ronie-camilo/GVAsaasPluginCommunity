using System.Collections.Generic;

namespace Gvinci.Plugin.Action
{
    public class GPluginAssasCommunity : IGPlugin
    {
        public string ID => "{C6926C40-F906-4BF9-0000-462296E7027E}";

        public string Name => "Asaas Integration Community";

        public string Description => "Plugin criado pela comunidade para completa integração do sistema de pagamento do Asaas";

        public string CompatibilityVersion => "2023";

        public string ProjectType => "CSHARP";

        public List<GPluginAction> Actions
        {
            get
            {
                return new List<GPluginAction>()
                {
                    new GPluginActionAsaasCustomerCreate(this),
                    new GPluginActionAsaasCustomerDelete(this),
                    new GPluginActionAsaasPaymentsCreate(this),
                    new GPluginActionAsaasPaymentsDelete(this),
                    new GPluginActionAsaasPaymentsRefund(this),
                    new GPluginActionAsaasSuscriptionsCreate(this),
                    new GPluginActionAsaasSuscriptionsDelete(this),
                };
            }
        }

        public List<GPluginDependency> GetDependenciesFiles()
        {
            return new List<GPluginDependency>()
            {
                new GPluginDependency() { FileName = "AsaasHelperCommunity.cs", DestinationRelativePath = "App_Code", AllowReplace = true },
                new GPluginDependency() { FileName = "AsaasModelCommunity.cs", DestinationRelativePath = "App_Code", AllowReplace = true },
                new GPluginDependency() { FileName = "RestSharp.dll", DestinationRelativePath = "bin", AllowReplace = true }
            };
        }
    }
}
