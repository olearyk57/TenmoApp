﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient
{
    public class APIService
    {
        private readonly string API_URL = "";
        private readonly RestClient client;
        private ApiUser user = new ApiUser();

        public int GetAccountId(int userId)
        {
            //client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + "account/" + userId);
            IRestResponse<int> response = client.Get<int>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: $" + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }

        }

        public decimal GetBalance(int userId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + "account/balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: $" + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        //Method Case 1
        public IList<User> GetUsers()
        {
            //client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + "transfer/users");
            IRestResponse<IList<User>> response = client.Get<IList<User>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: $" + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }
        
        public void CreateTransaction(TransferRequest transferRequest)
        {
            //client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + "transfer");
            request.AddJsonBody(transferRequest);
            IRestResponse response = client.Post(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: $" + (int)response.StatusCode);
            }

            Console.WriteLine("Everything works I think?");

        }

        public IList<TransferReceipt> GetTransfersForLoggedInUser(int accountId)
        {
            RestRequest request = new RestRequest(API_URL + "transfer/history");
            IRestResponse<IList<TransferReceipt>> response = client.Get<IList<TransferReceipt>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: $" + (int)response.StatusCode);
            }

            return response.Data;
        }


        //CTOR
        public APIService(string api_url)
        {
            API_URL = api_url;
            client = new RestClient();
        }
    }
    //DANGER ZONE
}
