using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

public static class GvinciAsaasCommunity
{
    public static AsaasModelCommunity.CustomerResponse Asaas_CustomerCreate(string Token = "", string Name = "", string CpfCnpj = "", string Email = "", string Phone = "", string Mobilephone = "", string Address = "", string AddressNumber = "", string Complement = "", string Province = "", string PostalCode = "", string ExternalReference = "", bool NotificationDisabled = false, string AdditionalEmails = "", string MunicipalInscription = "", string StateInscription = "", string Observations = "", string GroupName = "", string Environment = "S")
    {
        try
        {
            AsaasModelCommunity.CustomerResponseList ListCustomers = Asaas_CustomerSearch(Token: Token, CpfCnpj: CpfCnpj, Environment: Environment);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");

            string CustomerID = (ListCustomers.totalCount > 0 ? ListCustomers.data[0].id : "");
            string url = LinkAsaas + (ListCustomers.totalCount > 0 ? "/api/v3/customers/" + ListCustomers.data[0].id : "/api/v3/customers");

            var client = new RestClient(url);

            var request = (CustomerID == "" ? new RestRequest(Method.POST) : new RestRequest(Method.PUT));

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var customerRequest = new AsaasModelCommunity.CustomerRequest 
            {
                id = CustomerID,
                name = Name,
                cpfCnpj = CpfCnpj,
                email = Email,
                phone = Phone,
                mobilePhone = Mobilephone,
                address = Address,
                addressNumber = AddressNumber,
                complement = Complement,
                province = Province,
                postalCode = PostalCode,
                externalReference = ExternalReference,
                notificationDisabled = NotificationDisabled,
                additionalEmails = AdditionalEmails,
                municipalInscription = MunicipalInscription,
                stateInscription = StateInscription,
                observations = Observations,
                groupName = GroupName
            };

            request.AddJsonBody(customerRequest);

            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.CustomerResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponse>(response.Content);
            customerResponse.content = response.Content;

            return customerResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.CustomerRemoveResponse Asaas_CustomerRemove(string Token = "", string CustomerID = "", string Environment = "S")
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/customers/" + CustomerID);

            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.CustomerRemoveResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerRemoveResponse>(response.Content);
            customerResponse.content = response.Content;

            return customerResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.CustomerRemoveResponse Asaas_CustomerRestore(string Token = "", string CustomerID = "", string Environment = "S")
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/customers/" + CustomerID + "/restore");

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.CustomerRemoveResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerRemoveResponse>(response.Content);
            customerResponse.content = response.Content;

            return customerResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.CustomerResponseList Asaas_CustomerRecover(string Token = "", string CustomerID = "", string Name = "", string CpfCnpj = "", string Email = "", string GroupName = "", string ExternalReference = "", string Environment = "S")
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            string url = "", FilterArgs = "";

            if (CustomerID != "") 
            {
                url = LinkAsaas + "/api/v3/customers/" + CustomerID;
            }
            else
            {
                url = LinkAsaas + "/api/v3/customers?";
                FilterArgs += (Name != "" ? "name=" + Name : "");
                FilterArgs += (Email != "" ? (FilterArgs != "" ? "&" : "") + "email=" + Email : "");
                FilterArgs += (CpfCnpj != "" ? (FilterArgs != "" ? "&" : "") + "cpfCnpj=" + CpfCnpj : "");
                FilterArgs += (GroupName != "" ? (FilterArgs != "" ? "&" : "") + "groupName=" + GroupName : "");
                FilterArgs += (ExternalReference != "" ? (FilterArgs != "" ? "&" : "") + "externalReference=" + ExternalReference : "");
                url += FilterArgs;

                if (FilterArgs == "")
                {
                    return null;
                }
            }

            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var response = client.Get(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.CustomerResponseList listCustomers = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponseList>(response.Content);
            listCustomers.content = response.Content.ToString();

            return listCustomers;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_PaymentsCreate(string Token, string Name, string CpfCnpj, string Email, string Phone, string Mobilephone, string PostalCode, string AddressNumber, string ExternalReference, DateTime Vencimento, decimal Valor, string Descricao, string DocRef, string InterestValue, string FineValue, string Environment)
    {
        var customerResponse = Asaas_CustomerCreate(Token, Name, CpfCnpj, Email, Phone, Mobilephone, PostalCode, AddressNumber, ExternalReference, Environment);
        return Asaas_PaymentsCreate(Token, customerResponse.id, Vencimento, Valor, Descricao, DocRef, InterestValue, FineValue, Environment);


    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_PaymentsCreate(string Token, string CustomerID, DateTime DueDate, decimal Value, string Description, string DocRef, string InterestValue, string FineValue, string Environment)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/payments/");

            var request = new RestRequest();
            request.AddHeader("access_token", Token);
            request.AddHeader("Content-Type", "application/json");

            var cobrancaRequest = new AsaasModelCommunity.PaymentsBillingPixRequest();
            cobrancaRequest.customer = CustomerID;
            cobrancaRequest.dueDate = DueDate.ToString("yyyy-MM-dd");
            cobrancaRequest.value = (Decimal)Value;
            cobrancaRequest.description = Description;
            cobrancaRequest.externalReference = DocRef;
            cobrancaRequest.billingType = "BOLETO";
            cobrancaRequest.interest = new AsaasModelCommunity.Interest { value = int.Parse(InterestValue) }; // cobranca.PercentualJurosMes;
            cobrancaRequest.fine = new AsaasModelCommunity.Fine { value = int.Parse(FineValue) }; //cobranca.PercentualMulta;
            request.AddJsonBody(cobrancaRequest);


            var response = client.Post(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.PaymentsResponse PaymentsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);

            return PaymentsResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_PaymentsDelete(string Token, string PaymentsID, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/payments/" + PaymentsID);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var response = client.Delete(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.PaymentsResponse PaymentsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);

            return PaymentsResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_PaymentsConsult(string Token, string PaymentsID, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/payments/" + PaymentsID);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);


            var response = client.Post(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.PaymentsResponse PaymentsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);

            return PaymentsResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_PaymentsRefund(string Token, string PaymentsID, decimal Value, string Description, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/payments/" + PaymentsID + "/refund");

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var body = @"{" + "\n" +
                       @"    ""value"": " + Value.ToString() + "\n" +
                       @"    ""description"": " + Description + "\n" +
                       @"}";

            request.AddBody(body);

            var response = client.Post(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.PaymentsResponse PaymentsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);

            return PaymentsResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_SubscriptionsCreate(string Token, string CustomerID, string BillingType, DateTime NextDueDate, decimal Value, string Cycle, string Description, decimal DiscountValue, DateTime DueDateLimitDays, decimal FineValue, decimal InterestValue, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/subscriptions");

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var body = @"{" + "\n" +
                       @"    ""customer"": " + CustomerID + ",\n" +
                       @"    ""billingType"": " + BillingType + ",\n" +
                       @"    ""nextDueDate"": " + NextDueDate.ToString() + ",\n" +
                       @"    ""value"": " + Value.ToString() + ",\n" +
                       @"    ""cycle"": " + Cycle + ",\n" +
                       @"    ""description"": " + Description + ",\n" +
                       @"    ""discount"": " + "{\n" +
                       @"       ""value"": " + DiscountValue.ToString() + ",\n" +
                       @"       ""dueDateLimitDays"": " + DueDateLimitDays.ToString() + ",\n" +
                       @"    },\n" +
                       @"    ""fine"": " + "{\n" +
                       @"       ""value"": " + FineValue.ToString() + ",\n" +
                       @"    },\n" +
                       @"    ""interest"": " + "{\n" +
                       @"       ""value"": " + InterestValue.ToString() + ",\n" +
                       @"    },\n" +
                       @"}";

            request.AddBody(body);

            var response = client.Post(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.PaymentsResponse SubscriptionsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);

            return SubscriptionsResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_SubscriptionsDelete(string Token, string SubscriptionsID, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/subscriptions/" + SubscriptionsID);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var response = client.Delete(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.PaymentsResponse SubscriptionsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);

            return SubscriptionsResponse;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}