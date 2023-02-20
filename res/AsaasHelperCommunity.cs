using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

public static class GvinciAsaasCommunity
{

    public static string Asaas_CustomerCreateOrUpdate(string Token, string Name, string CpfCnpj, string Email, string Phone, string Mobilephone, string PostalCode, string AddressNumber, string ExternalReference, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string linkAsaas = (Environment == "P" ? "https://www.asaas.com/api/v3/" : "https://sandbox.asaas.com/api/v3/");
            string url = linkAsaas + "/customers?email=" + (Email);
            var client = new RestClient(url);
            var request = new RestRequest();
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

                AsaasModelCommunity.CustomerResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponse>(response.Content);

                if (ReturnType == "CustomerID")
                    return customerResponse.id;
                else if (ReturnType == "Content")
                    return response.Content;
                else
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

    public static string Asaas_CustomerDelete(string Token, string CustomerID, string Environment, string ReturnType)
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");
            var client = new RestClient(LinkAsaas + "/api/v3/payments/" + CustomerID);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);

            var response = client.Delete(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            AsaasModelCommunity.CustomerResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponse>(response.Content);

            if (ReturnType == "CustomerState")
                return customerResponse.id;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return customerResponse.state;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Asaas_PaymentsCreate(string Token, string Name, string CpfCnpj, string Email, string Phone, string Mobilephone, string PostalCode, string AddressNumber, string ExternalReference, DateTime Vencimento, decimal Valor, string Descricao, string DocRef, string InterestValue, string FineValue, string Environment, string ReturnType)
    {
        var CustomerID = Asaas_CustomerCreateOrUpdate(Token, Name, CpfCnpj, Email, Phone, Mobilephone, PostalCode, AddressNumber, ExternalReference, Environment, "CustomerID");
        if (ReturnType == "PaymentsID")
            return Assas_PaymentsCreate(CustomerID, Token, Vencimento, Valor, Descricao, DocRef, InterestValue, FineValue, Environment, "PaymentsID");
        else if (ReturnType == "Content")
            return Assas_PaymentsCreate(CustomerID, Token, Vencimento, Valor, Descricao, DocRef, InterestValue, FineValue, Environment, "Content");
        else
            return Assas_PaymentsCreate(CustomerID, Token, Vencimento, Valor, Descricao, DocRef, InterestValue, FineValue, Environment, "PaymentsID");


    }

    public static string Assas_PaymentsCreate(string CustomerID, string Token, DateTime DueDate, decimal Value, string Description, string DocRef, string InterestValue, string FineValue, string Environment, string ReturnType)
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

            if (ReturnType == "PaymentsID")
                return PaymentsResponse.id;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return PaymentsResponse.id;


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Asaas_PaymentsDelete(string Token, string PaymentsID, string Environment, string ReturnType)
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

            if (ReturnType == "PaymentsID")
                return PaymentsResponse.id;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return PaymentsResponse.id;

        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Asaas_PaymentsConsult(string Token, string PaymentsID, string Environment, string ReturnType)
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

            if (ReturnType == "PaymentsStatus")
                return PaymentsResponse.status;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return PaymentsResponse.status;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Asaas_PaymentsRefund(string Token, string PaymentsID, decimal Value, string Description,string Environment, string ReturnType)
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

            if (ReturnType == "PaymentsStatus")
                return PaymentsResponse.status;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return PaymentsResponse.status;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public static string Asaas_SubscriptionsCreate(string Token, string CustomerID, string BillingType, DateTime NextDueDate, decimal Value, string Cycle, string Description, decimal DiscountValue, DateTime DueDateLimitDays, decimal FineValue, decimal InterestValue, string Environment, string ReturnType)
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

            if (ReturnType == "SubscriptionsID")
                return SubscriptionsResponse.id;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return SubscriptionsResponse.id;

            SubscriptionsResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.PaymentsResponse>(response.Content);
            return SubscriptionsResponse.id;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string Asaas_SubscriptionsDelete(string Token, string SubscriptionsID, string Environment, string ReturnType)
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

            if (ReturnType == "SubscriptionsID")
                return SubscriptionsResponse.id;
            else if (ReturnType == "Content")
                return response.Content;
            else
                return SubscriptionsResponse.id;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}