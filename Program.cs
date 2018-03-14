// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Evolve Software Ltd">
//   Copyright (c) Evolve Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Zapster.Console
{
    using System;

    using Zapster.Api.Client;
    using Zapster.Api.Client.Model;

    /// <summary>
    /// Console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The API endpoint.
        /// </summary>
        private static string apiEndpoint;

        /// <summary>
        /// The account guid.
        /// </summary>
        private static Guid account;

        /// <summary>
        /// The known transaction.
        /// </summary>
        private static Guid knownTransaction;

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            apiEndpoint = "https://zapster.io/api";

            Console.WriteLine("Please ensure you've created an account on Zapster:");

            account = Guid.Parse("XXX");
            knownTransaction = Guid.Parse("XXX");

            Console.WriteLine("Please select an operation to test:");
            Console.WriteLine();
            Console.WriteLine("1: Unauthenticated Heartbeat");
            Console.WriteLine("2: Create Test Wallets");
            Console.WriteLine("3: Currency Exchange");
            Console.WriteLine("4: Create Transaction");
            Console.WriteLine("5: Get Transaction");
            Console.WriteLine();
            Console.WriteLine("exit: Close Console");

            while (true)
            {
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        UnathenticatedHeartbeat();
                        break;
                    case "2":
                        CreateTestWallets();
                        break;
                    case "3":
                        CurrencyExchange();
                        break;
                    case "5":
                        CreateTransaction();
                        break;
                    case "6":
                        GetTransaction();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void CurrencyExchange()
        {
            using (var client = new ZapsterApiClient(apiEndpoint))
            {
                Console.WriteLine("\nRequest: {0}/exchange", apiEndpoint);
                var result = client.Exchange.Calculate(account, CurrencyCode.CHF, 9.990M);
                Console.WriteLine("\t- Currency Exchange Data:");
                Console.WriteLine("\t rate ({0})", result.Rate);
                Console.WriteLine("\t amount ({0})", result.Amount);
            }
        }

        private static void CreateTestWallets()
        {
            using (var client = new ZapsterApiClient(apiEndpoint))
            {
                Console.WriteLine("\nRequest: {0}/wallets", apiEndpoint);
                foreach (var wallet in client.Wallets.CreateTestWallets().Wallets)
                {
                    Console.WriteLine("\t- Wallet Data:");
                    Console.WriteLine("\t Secret ({0})", wallet.PrivateKey);
                    Console.WriteLine("\t Address ({0})", wallet.Address);
                    Console.WriteLine();
                }
            }
        }

        private static void UnathenticatedHeartbeat()
        {
            using (var client = new ZapsterApiClient(apiEndpoint))
            {
                Console.WriteLine("\nRequest: {0}/heartbeat", apiEndpoint);
                var heartbeat = client.HealthCheck.Heartbeat();
                Console.WriteLine("{0}", heartbeat.Version);
                Console.WriteLine("{0}", heartbeat.Alive);
            }
        }

        private static void CreateTransaction()
        {
            using (var client = new ZapsterApiClient(apiEndpoint))
            {
                Console.WriteLine("\nRequest: {0}/transactions", apiEndpoint);

                var result = client.Transactions.Create(account, CurrencyCode.XRP, 9.990M);
                
                Console.WriteLine("\t- Transaction Data:");
                Console.WriteLine("\t transaction ({0})", result.Transaction.Id);
                Console.WriteLine("\t account ({0})", result.Transaction.Account);
                Console.WriteLine("\t pin ({0})", result.Transaction.PinCode);
                Console.WriteLine("\t amount ({0})", result.Transaction.Amount);
                Console.WriteLine("\t address ({0})", result.Transaction.Address);
                Console.WriteLine("\t created ({0})", result.Transaction.Created);
                Console.WriteLine("\t status ({0})", result.Transaction.Status);
            }
        }

        private static void GetTransaction()
        {
            using (var client = new ZapsterApiClient(apiEndpoint))
            {
                Console.WriteLine("\nRequest: {0}/transactions", apiEndpoint);
                var result = client.Transactions.Get(knownTransaction);
                Console.WriteLine("\t- Transaction Data:");
                Console.WriteLine("\t transaction ({0})", result.Transaction.Id);
                Console.WriteLine("\t account ({0})", result.Transaction.Account);
                Console.WriteLine("\t pin ({0})", result.Transaction.PinCode);
                Console.WriteLine("\t amount ({0})", result.Transaction.Amount);
                Console.WriteLine("\t address ({0})", result.Transaction.Address);
                Console.WriteLine("\t created ({0})", result.Transaction.Created);
                Console.WriteLine("\t status ({0})", result.Transaction.Status);
                Console.WriteLine("\t blockchain tx ({0})", result.Transaction.BlockchainId);
                Console.WriteLine("\t executed ({0})", result.Transaction.BlockchainExecutedTime);
                Console.WriteLine("\t source ({0})", result.Transaction.Source);
                Console.WriteLine("\t callback ({0})", result.Transaction.CallbackUrl);
            }
        }
    }
}
