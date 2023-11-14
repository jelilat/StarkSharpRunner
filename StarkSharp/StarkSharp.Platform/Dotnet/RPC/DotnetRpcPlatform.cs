﻿using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdronCore.Connectors.Components;
using AdronCore.Rpc;
using AdronCore.Rpc.Modules.Transactions;
using AdronCore.Tools.Notification;

namespace AdronCore.Platforms.Dotnet.RPC
{
    public class DotnetRpcPlatform : DotnetPlatform
    {
        public override async void CallContract(ContractInteraction contractInteraction, Action<string> successCallback, Action<string> errorCallback)
        {
            if (contractInteraction != null)
            {
                var requestdata = JsonRpcHandler.GenerateContractRequestData(contractInteraction.ContractAdress,contractInteraction.EntryPoint,contractInteraction.CallData);
                var response = await SendPostRequest(requestdata);

                if (response == null || response.error != null)
                {
                    errorCallback?.Invoke(response?.error?.message ?? "Unknown error");
                }
                else
                {
                    successCallback?.Invoke(JsonConvert.SerializeObject(response.result));
                }
            }
            else
            {
                errorCallback?.Invoke("Insufficient callContractData parameters");
            }
        }

        public override async void SendTransaction(Platform platform, TransactionInteraction transactionInteraction, Action<JsonRpcResponse> successCallback, Action<JsonRpcResponse> errorCallback)
        {
            if (transactionInteraction == null || string.IsNullOrEmpty(transactionInteraction.FunctionName))
            {
                errorCallback?.Invoke(new JsonRpcResponse
                {
                    error = new JsonRpcError { code = -1, message = "Insufficient callContractData parameters" }
                });
                return;
            }

            try
            {
                Transaction transaction = new Transaction(platform);
                var requestData = transaction.CreateTransaction(transactionInteraction);

                var response = await SendPostRequest(requestData);
                transaction.OnNonceComplete(platform, transactionInteraction, response);
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke(new JsonRpcResponse
                {
                    error = new JsonRpcError { code = -1, message = $"An error occurred: {ex.Message}" }
                });
            }
        }

        public override async void PlatformRequest(JsonRpc requestData, Action<JsonRpcResponse> Callback)
        {
            if (requestData == null)
            {
                Callback?.Invoke(new JsonRpcResponse
                {
                    error = new JsonRpcError { code = -1, message = "Request data is null." }
                });
                return;
            }

            try
            {
                var response = await SendPostRequest(requestData);
                if (response == null || response.error != null)
                {
                    Callback?.Invoke(new JsonRpcResponse
                    {
                        error = new JsonRpcError { message = response?.error?.message ?? "Unknown error" }
                    });
                }
                else
                {
                    string resultString = response.result as string;
                    if (!string.IsNullOrEmpty(resultString))
                    {
                        Callback?.Invoke(new JsonRpcResponse { result = resultString });
                    }
                    else
                    {

                        Console.WriteLine(requestData);
                    }
                }
            }
            catch (Exception ex)
            {
                Callback?.Invoke(new JsonRpcResponse
                {
                    error = new JsonRpcError { code = -1, message = $"An error occurred: {ex.Message}" }
                });
            }
        }


        public async Task<JsonRpcResponse> SendPostRequest(JsonRpc requestData)
        {
            string json = JsonConvert.SerializeObject(requestData);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending POST request to {Settings.Settings.apiurl} with data: {json}");

                try
                {
                    var response =  httpClient.PostAsync(Settings.Settings.apiurl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JsonRpcResponse>(responseText);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return new JsonRpcResponse
                        {
                            error = new JsonRpcError { code = -1, message = $"Error: {response.StatusCode} - {response.ReasonPhrase}" }
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return new JsonRpcResponse
                    {
                        error = new JsonRpcError { code = -1, message = $"An error occurred: {ex.Message}" }
                    };
                }
            }
        }

        public override void PlatformLog(string LogMessage, NotificationType notitype) { Notify.ShowNotification(LogMessage, notitype, NotificationPlatform.DotNet); }
    }
}
