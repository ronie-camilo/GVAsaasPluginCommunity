using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

public static class GvinciAsaasCommunity
{
    //Metodo para criação de um novo cliente ou atualização de um cliente existente na base do gateway Asaas
    public static AsaasModelCommunity.CustomerResponse Asaas_CustomerSynchronize(string Environment = "S", string Token = "", string CustomerID = "", string Name = "", string CpfCnpj = "", string Email = "", string Phone = "", string Mobilephone = "", string Address = "", string AddressNumber = "", string Complement = "", string Province = "", string PostalCode = "", string ExternalReference = "", bool NotificationDisabled = false, string AdditionalEmails = "", string MunicipalInscription = "", string StateInscription = "", string Observations = "", string GroupName = "")
    {
        try
        {
            //Verifica os dados requeridos (informados o Token e Nome e CPF ou então informado o Token e CustomerID)
            if ((Token != "") && (((Name != "") && (CpfCnpj != "")) || (CustomerID != "")))
            {
                //Declara variavel para do CustomerID
                string tempCustomerID = "";

                //Verifica se foi informado um CustomerID
                if (CustomerID != "")
                {
                    //Atribui o valor do CustomerID para a variavel de tratamento
                    tempCustomerID = CustomerID;
                }
                else
                {
                    //Declara variavel para receber os dados de cliente existente
                    AsaasModelCommunity.CustomerResponse customer = Asaas_CustomerRecover(Environment: Environment, Token: Token, Name: Name, CpfCnpj: CpfCnpj);
                    if (customer != null)
                    {
                        //Se conseguiu recuperar o CustomerID, atribui para a variavel temporaria
                        tempCustomerID = customer.id;
                    }
                }

                //Declara o protocolo de segurança
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                //Declara e verifica o ambiente será utilizado
                string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");

                //Declara adequadamente a url que será utilizada na chamada da API
                string url = LinkAsaas + (tempCustomerID != "" ? "/api/v3/customers/" + tempCustomerID : "/api/v3/customers");

                //Declara o cliente que fará a chamada da API
                var client = new RestClient(url);

                //Declara adequadamente o tipo de requisição que será utilizada
                var request = (tempCustomerID == "" ? new RestRequest(Method.POST) : new RestRequest(Method.PUT));

                //Declara e prepara as infomrações que serão enviadas na chamada da API
                var customerRequest = new AsaasModelCommunity.CustomerRequest
                {
                    id = tempCustomerID,
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

                //Adiciona as informações necessárias no cabeçario da requisição
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("access_token", Token);

                //Insere as informações do cliente no corpo da requisição
                request.AddJsonBody(customerRequest);

                //Executa a chamada da API
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                //Prepara os dados de retorno
                AsaasModelCommunity.CustomerResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponse>(response.Content);
                customerResponse.content = response.Content;

                //Retrorna as informações do cliente que foi criado ou atualizado
                return customerResponse;
            }
            else
            {
                //Se não foi informado as dados requeridos será retornado Null
                return null;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Metodo para remover um cliente na base do gateway Asaas
    public static AsaasModelCommunity.CustomerRemoveResponse Asaas_CustomerRemove(string Environment = "S", string Token = "", string CustomerID = "")
    {
        try
        {
            //Verifica os dados requeridos (informados o Token e CustomerID)
            if ((Token != "") && (CustomerID != ""))
            {
                //Declara o protocolo de segurança
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                //Declara e verifica o ambiente será utilizado
                string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");

                //Declara adequadamente a url que será utilizada na chamada da API
                string url = LinkAsaas + "/api/v3/customers/" + CustomerID;

                //Declara o cliente que fará a chamada da API
                var client = new RestClient(url);

                //Declara adequadamente o tipo de requisição que será utilizada
                var request = new RestRequest(Method.DELETE);

                //Adiciona as informações necessárias no cabeçario da requisição
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("access_token", Token);

                //Executa a chamada da API
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                //Prepara os dados de retorno
                AsaasModelCommunity.CustomerRemoveResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerRemoveResponse>(response.Content);
                customerResponse.content = response.Content;

                //Retrorna as informações do cliente que foi removido
                return customerResponse;
            }
            else
            {
                //Se não foi informado as dados requeridos será retornado Null
                return null;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Metodo para restaurar um cliente removido na base do gateway Asaas
    public static AsaasModelCommunity.CustomerRemoveResponse Asaas_CustomerRestore(string Environment = "S", string Token = "", string CustomerID = "")
    {
        try
        {
            //Verifica os dados requeridos (informados o Token e CustomerID)
            if ((Token != "") && (CustomerID != ""))
            {
                //Declara o protocolo de segurança
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                //Declara e verifica o ambiente será utilizado
                string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");

                //Declara adequadamente a url que será utilizada na chamada da API
                string url = LinkAsaas + "/api/v3/customers/" + CustomerID + "/restore";

                //Declara o cliente que fará a chamada da API
                var client = new RestClient(url);

                //Declara adequadamente o tipo de requisição que será utilizada
                var request = new RestRequest(Method.POST);

                //Adiciona as informações necessárias no cabeçario da requisição
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("access_token", Token);

                //Executa a chamada da API
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                //Prepara os dados de retorno
                AsaasModelCommunity.CustomerRemoveResponse customerResponse = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerRemoveResponse>(response.Content);
                customerResponse.content = response.Content;

                //Retrorna as informações do cliente que foi restaurado
                return customerResponse;
            }
            else
            {
                //Se não foi informado as dados requeridos será retornado Null
                return null;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Metodo para recuperar informações um cliente na base do gateway Asaas
    public static AsaasModelCommunity.CustomerResponse Asaas_CustomerRecover(string Environment = "S", string Token = "", string CustomerID = "", string Name = "", string CpfCnpj = "", string Email = "", string GroupName = "", string ExternalReference = "")
    {
        try
        {
            //Verifica os dados requeridos (informados o Token e Nome e CPF ou então informado o Token e CustomerID)
            if ((Token != "") && (Name != "" && CpfCnpj != "") || (CustomerID != ""))
            {
                //Declara o protocolo de segurança
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                //Declara e verifica o ambiente será utilizado
                string LinkAsaas = (Environment == "P" ? "https://www.asaas.com" : "https://sandbox.asaas.com");

                //Declara as variaveis que seram utilizadas pela url e filtro
                string url = "", FilterArgs = "";

                //Verifica se foi informado o CostumerID
                if (CustomerID != "")
                {
                    //Monta adequadamente a url que será utilizada na chamada da API, tendo como base o CustomerID
                    url = LinkAsaas + "/api/v3/customers/" + CustomerID;
                }
                else
                {
                    //inicia a montagem da url que será utilizada na chamada da API, tendo como base o filtro com as informações fornecidas
                    url = LinkAsaas + "/api/v3/customers?";

                    //Montagem do filtro com as informações fornecedas
                    FilterArgs += (Name != "" ? "name=" + Name : "");
                    FilterArgs += (Email != "" ? (FilterArgs != "" ? "&" : "") + "email=" + Email : "");
                    FilterArgs += (CpfCnpj != "" ? (FilterArgs != "" ? "&" : "") + "cpfCnpj=" + CpfCnpj : "");
                    FilterArgs += (GroupName != "" ? (FilterArgs != "" ? "&" : "") + "groupName=" + GroupName : "");
                    FilterArgs += (ExternalReference != "" ? (FilterArgs != "" ? "&" : "") + "externalReference=" + ExternalReference : "");

                    //Verifica se o filtro foi montado adequadamente
                    if (FilterArgs == "")
                    {
                        //Se não montou o filtro será retornado Null
                        return null;
                    }

                    //Conclui a montagem da url que será utilizada na chamada da API, tendo como base o filtro com as informações fornecidas
                    url += FilterArgs;

                }

                //Declara o cliente que fará a chamada da API
                var client = new RestClient(url);

                //Declara adequadamente o tipo de requisição que será utilizada
                var request = new RestRequest(Method.GET);

                //Adiciona as informações necessárias no cabeçario da requisição
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("access_token", Token);

                //Executa a chamada da API
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                //Declara a variavel costumer que será retornada
                AsaasModelCommunity.CustomerResponse customer = null;

                if (CustomerID != "")
                {
                    //Prepara os dados de retorno
                    customer = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponse>(response.Content);
                    customer.content = response.Content;
                }
                else
                {
                    //Declara a variavel listCostumers que será utilizada na montagem do retorno
                    AsaasModelCommunity.CustomerResponseList listCustomers = JsonConvert.DeserializeObject<AsaasModelCommunity.CustomerResponseList>(response.Content);

                    //Verifica se foi retornado pelo menos um cliente
                    if (listCustomers.totalCount > 0)
                    {
                        //Prepara os dados de retorno
                        customer = listCustomers.data[0];
                        customer.content = response.Content;
                    }
                }

                //Retrorna as informações do cliente que foi recuperado
                return customer;
            }
            else
            {
                //Se não foi informado as dados requeridos será retornado Null
                return null;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static AsaasModelCommunity.PaymentsResponse Asaas_PaymentsCreate(string Token, string Name, string CpfCnpj, string Email, string Phone, string Mobilephone, string PostalCode, string AddressNumber, string ExternalReference, DateTime Vencimento, decimal Valor, string Descricao, string DocRef, string InterestValue, string FineValue, string Environment)
    {
        var customerResponse = Asaas_CustomerSynchronize(Token, Name, CpfCnpj, Email, Phone, Mobilephone, PostalCode, AddressNumber, ExternalReference, Environment);
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