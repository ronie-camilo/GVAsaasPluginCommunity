using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using static AsaasModelCommunity;

public static class GvinciAsaasCommunity
{

    public static string Assas_CustomerCreateOrUpdate(string Token, string Name, string CpfCnpj, string Email, string Phone, string Mobilephone, string PostalCode, string AddressNumber, string ExternalReference, string Environment)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string linkAsaas = (Environment == "P" ? "https://www.asaas.com/api/v3/" : "https://sandbox.asaas.com/api/v3/");
            string url = linkAsaas + "/customers?email=" + (Email);
            var client = new RestClient(url);
            var request = new RestRequest();
            AsaasModelCommunity.CustomerResponse customerResponse = new AsaasModelCommunity.CustomerResponse();
            request.AddHeader("access_token", Token);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                AsaasModelCommunity.CustomerResponseList listCustomers = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponseList>(response.Content);
                if (listCustomers == null)
                {
                    throw new Exception("Erro - " + response.ErrorMessage); ;
                }
                if (listCustomers.totalCount == 0)
                {
                    request = new RestRequest(Method.POST);
                    url = linkAsaas + "/customers";
                }
                else
                {
                    request = new RestRequest(Method.PUT);
                }
                request.AddHeader("access_token", Token);
                client = new RestClient(url);
                var customerRequest = new AsaasModelCommunity.CustomerRequest();
                if (listCustomers.totalCount > 0) customerRequest.id = listCustomers.data[0].id;
                customerRequest.name = Name;
                customerRequest.cpfCnpj = CpfCnpj;
                customerRequest.email = Email;
                customerRequest.phone = Phone;
                customerRequest.mobilePhone = Mobilephone;
                customerRequest.addressNumber = AddressNumber;
                customerRequest.postalCode = PostalCode;
                customerRequest.externalReference = ExternalReference;
                request.AddJsonBody(customerRequest);
                response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }
                customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponse>(response.Content);
                return customerResponse.id;
            }
            else
            {
                throw new Exception(response.Content);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Assas_PaymentsCreate(string Token, string Name, string CpfCnpj, string Email, string Phone, string Mobilephone, string PostalCode, string AddressNumber, string ExternalReference, string LinkAsaas, DateTime Vencimento, decimal Valor, string Descricao, string DocRef)
    {
        var CustomerID = Assas_CustomerCreateOrUpdate(Token, Name, CpfCnpj, Email, Phone, Mobilephone, PostalCode, AddressNumber, ExternalReference, LinkAsaas);
        return Assas_PaymentsCreate(CustomerID, LinkAsaas, Token, Vencimento, Valor, Descricao, DocRef);
    }

    public static string Assas_PaymentsCreate(string CustomerID, string Environment, string Token, DateTime DueDate, decimal Value, string Description, string DocRef)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com/api/v3/" : "https://sandbox.asaas.com/api/v3/");
            var client = new RestClient(LinkAsaas + "/payments");
            var request = new RestRequest();
            request.AddHeader("access_token", Token);
            var cobrancaRequest = new AsaasModelCommunity.CobrancaBoletoPixRequest();
            cobrancaRequest.customer = CustomerID;
            cobrancaRequest.dueDate = DueDate.ToString("yyyy-MM-dd");
            cobrancaRequest.value = (Decimal)Value;
            cobrancaRequest.description = Description;
            cobrancaRequest.externalReference = DocRef;
            cobrancaRequest.billingType = "BOLETO";
            cobrancaRequest.interest = new AsaasModelCommunity.Interest { value = 2 }; // cobranca.PercentualJurosMes;
            cobrancaRequest.fine = new AsaasModelCommunity.Fine { value = 1 }; //cobranca.PercentualMulta;
            request.AddJsonBody(cobrancaRequest);
            AsaasModelCommunity.CobrancaResponse cobrancaResponse = new AsaasModelCommunity.CobrancaResponse();
            var response = client.Post(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            cobrancaResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CobrancaResponse>(response.Content);
            return cobrancaResponse.id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Assas_PaymentsDelete(string Token, string PaymentsID, string Environment)
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
        return response.Content;
    }

    public static string Assas_PaymentsConsult(string Token, string PaymentsID, string Environment)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        string LinkAsaas = (Environment == "P" ? "https://www.asaas.com/api/v3/customers" : "https://sandbox.asaas.com/api/v3/customers");
        var client = new RestClient(LinkAsaas + PaymentsID);
        var request = new RestRequest();
        request.AddHeader("access_token", Token);
        var response = client.Delete(request);
        if (!response.IsSuccessful)
        {
            throw new Exception(response.Content);
        }
        return response.Content;
    }

    public static string Assas_PaymentsRefund(string Token, string PaymentsID, decimal Value, string Description,string Environment)
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
        return response.Content;
    }

    public static string Asaas_SubscriptionsCreate(string Token, string CustomerID, string BillingType, DateTime NextDueDate, decimal Value, string Cycle, string Description, decimal DiscountValue, DateTime DueDateLimitDays, decimal FineValue, decimal InterestValue, string Environment)
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
        return response.Content;
    }

    public static string Asaas_SubscriptionsDelete(string Token, string CustomerID, string BillingType, DateTime DueDate, decimal Value, string Description, string DocRef, string Environment)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        string LinkAsaas = (Environment == "P" ? "https://www.asaas.com/api/v3/subscriptions" : "https://sandbox.asaas.com/api/v3/subscriptions");
        var client = new RestClient(LinkAsaas + PaymentsID);
        var request = new RestRequest();
        request.AddHeader("access_token", Token);
        var response = client.Delete(request);
        if (!response.IsSuccessful)
        {
            throw new Exception(response.Content);
        }
        return response.Content;
    }

}