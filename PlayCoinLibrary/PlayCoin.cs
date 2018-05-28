using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace PlayCoinLibrary
{
    public class PlayCoin
    {
        #region Declaration
        private Nethereum.Contracts.Contract contract;
        private Nethereum.Web3.Web3 web3;
        private User game = new User();
        private User player = new User();
        private const string BaseURL = "http://localhost:3000/";
        private String abi = @" [  
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""name"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""string""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""spender"",
        ""type"": ""address""
      },
      {
        ""name"": ""tokens"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""approve"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""totalSupply"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""sender"",
        ""type"": ""address""
      },
      {
        ""name"": ""player"",
        ""type"": ""address""
      },
      {
        ""name"": ""gameCoins"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""win"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""from"",
        ""type"": ""address""
      },
      {
        ""name"": ""to"",
        ""type"": ""address""
      },
      {
        ""name"": ""tokens"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""transferFrom"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""decimals"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""uint8""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [
      {
        ""name"": ""game"",
        ""type"": ""address""
      }
    ],
    ""name"": ""getGameUnitPrice"",
    ""outputs"": [
      {
        ""name"": ""gameUnit"",
        ""type"": ""uint256""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""game"",
        ""type"": ""address""
      },
      {
        ""name"": ""unit"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""setGameUnitPrice"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""_totalSupply"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""sender"",
        ""type"": ""address""
      },
      {
        ""name"": ""player"",
        ""type"": ""address""
      },
      {
        ""name"": ""gameCoins"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""lose"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""sender"",
        ""type"": ""address""
      },
      {
        ""name"": ""game"",
        ""type"": ""address""
      },
      {
        ""name"": ""coins"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""topUp"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [
      {
        ""name"": ""tokenOwner"",
        ""type"": ""address""
      }
    ],
    ""name"": ""balanceOf"",
    ""outputs"": [
      {
        ""name"": ""balance"",
        ""type"": ""uint256""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [],
    ""name"": ""acceptOwnership"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""sender"",
        ""type"": ""address""
      },
      {
        ""name"": ""player"",
        ""type"": ""address""
      },
      {
        ""name"": ""gameCoins"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""cashOut"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""owner"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""address""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""symbol"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""string""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""to"",
        ""type"": ""address""
      },
      {
        ""name"": ""tokens"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""transfer"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [
      {
        ""name"": ""tokenOwner"",
        ""type"": ""address""
      },
      {
        ""name"": ""game"",
        ""type"": ""address""
      }
    ],
    ""name"": ""gameBalanceOf"",
    ""outputs"": [
      {
        ""name"": ""balance"",
        ""type"": ""uint256""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""spender"",
        ""type"": ""address""
      },
      {
        ""name"": ""tokens"",
        ""type"": ""uint256""
      },
      {
        ""name"": ""data"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""approveAndCall"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [],
    ""name"": ""newOwner"",
    ""outputs"": [
      {
        ""name"": """",
        ""type"": ""address""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""tokenAddress"",
        ""type"": ""address""
      },
      {
        ""name"": ""tokens"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""transferAnyERC20Token"",
    ""outputs"": [
      {
        ""name"": ""success"",
        ""type"": ""bool""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""constant"": true,
    ""inputs"": [
      {
        ""name"": ""tokenOwner"",
        ""type"": ""address""
      },
      {
        ""name"": ""spender"",
        ""type"": ""address""
      }
    ],
    ""name"": ""allowance"",
    ""outputs"": [
      {
        ""name"": ""remaining"",
        ""type"": ""uint256""
      }
    ],
    ""payable"": false,
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""constant"": false,
    ""inputs"": [
      {
        ""name"": ""_newOwner"",
        ""type"": ""address""
      }
    ],
    ""name"": ""transferOwnership"",
    ""outputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""payable"": false,
    ""stateMutability"": ""nonpayable"",
    ""type"": ""constructor""
  },
  {
    ""payable"": true,
    ""stateMutability"": ""payable"",
    ""type"": ""fallback""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""name"": ""_from"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""name"": ""_to"",
        ""type"": ""address""
      }
    ],
    ""name"": ""OwnershipTransferred"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""name"": ""from"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""name"": ""to"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""name"": ""tokens"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""Transfer"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""name"": ""tokenOwner"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""name"": ""spender"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""name"": ""tokens"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""Approval"",
    ""type"": ""event""
  }
]";
        #endregion
        #region Initialization
        public async Task<Boolean> SignGame(String username, String Password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username",username),
                                new KeyValuePair<string, string>("password", Password)
            });
     var result = await client.PostAsync("loggame", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            if(!String.IsNullOrEmpty(resultContent)&&!resultContent.Equals("not a game")&&!resultContent.Equals("Invalid credentials"))
            {
                game = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(resultContent);
                if (game.Account_type.Equals("game"))
                {
                    return true;
                }
                else
                {
                    game = new User();
                    return false;   
                }
            }
            return false;
        }
        public async Task<Boolean> SignPlayer(String username, String Password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username",username),
                                new KeyValuePair<string, string>("password", Password)
            });
            var result = await client.PostAsync("loguser", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            if (!String.IsNullOrEmpty(resultContent) && !resultContent.Equals("not a simple user") && !resultContent.Equals("Invalid credentials"))
            {
                player = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(resultContent);
                if (player.Account_type.Equals("user"))
                {
                    return true;
                }
                else
                {
                    player = new User();
                    return false;
                }
            }
            return false;
        }
        public PlayCoin()
        {
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
            web3 = new Nethereum.Web3.Web3("https://ropsten.infura.io/6Eln6WdYNncc1lY1zSih");
            contract = web3.Eth.GetContract(abi, "0x9378e4cf1b5991eb5f1483c885426a9d23d87534");
        }
        #endregion
        #region UserFunctions
        // Get PlayCoin UserBalance
        public async Task<String> GetMyBalanceAsync()
        {
            if (!String.IsNullOrEmpty(player.Address))
            {



                var balancefunction = contract.GetFunction("balanceOf");
                var balance = await balancefunction.CallAsync<Int64>(player.Address);
                return balance.ToString();
            }
            else
            {
                throw new UserNotSetException();
            }
        }
        // Get PlayCoin UserBalance In a specific game
        public async Task<String> GetMyGameBalanceAsync()
        {
            if (String.IsNullOrEmpty(player.Address))
            {
                throw new UserNotSetException();
            }
            if (String.IsNullOrEmpty(game.Address))
            {
                throw new GameNotSetException();
            }

            var gamebalancefunction = contract.GetFunction("gameBalanceOf");
            var balance = await gamebalancefunction.CallAsync<Int64>(player.Address, game.Address);
            return balance.ToString();



        }
        // Topup Game  with  in-game coin value 
        public async Task<String> TopUpAsync(double coins)
        {
            if (String.IsNullOrEmpty(player.Address))
            {
                throw new UserNotSetException();
            }
            if (String.IsNullOrEmpty(game.Address))
            {
                throw new GameNotSetException();
            }

            var topUpFuntion = contract.GetFunction("topUp");
            var privateKey = player.Private_key;
            var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(player.Address);
            var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
            var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, contract.Address, new System.Numerics.BigInteger(0), txCount.Value, gasPrice, 90000, topUpFuntion.GetData(player.Address, game.Address, coins * 10000));

            var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("tx",txId),
                new KeyValuePair<string, string>("receiver", game.Id.ToString()),
                new KeyValuePair<string, string>("sender", player.Id.ToString()),
                new KeyValuePair<string, string>("ammount", coins.ToString()),
                new KeyValuePair<string, string>("type", "topup")

            });
            var result = await client.PostAsync("addtransaction", content);
            string resultContent = await result.Content.ReadAsStringAsync();

            return txId;

        }
        // set  in-game coin value 
        public async Task<String> SetGameUnitAsync(int unit)
        {

            if (!String.IsNullOrEmpty(game.Address))
            {

                var setGameUnitPrice = contract.GetFunction("setGameUnitPrice");
                var privateKey = game.Private_key;
                var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(game.Address);
                var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
                var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, contract.Address, new System.Numerics.BigInteger(0), txCount.Value, gasPrice, 90000, setGameUnitPrice.GetData(game.Address, unit));

                var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);


                return txId;
            }
            else
            {
                throw new GameNotSetException();
            }
        }
        // GET  in-game coin value 
        public async Task<String> GetGameUnitAsync()
        {
            if (!String.IsNullOrEmpty(game.Address))
            {


                //var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
                //var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(player);
                /// var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, gameAddress, new System.Numerics.BigInteger(coins), txCount.Value);
                var topupfunction = contract.GetFunction("getGameUnitPrice");
                var unit = await topupfunction.CallAsync<int>(game.Address);
                return unit.ToString();
            }
            else
            {
                throw new GameNotSetException();
            }
        }
        public async Task<String> CashoutAsync(double coins)
        {
            if (String.IsNullOrEmpty(player.Address))
            {
                throw new UserNotSetException();
            }
            if (String.IsNullOrEmpty(game.Address))
            {
                throw new GameNotSetException();
            }

            var cashOutFuntion = contract.GetFunction("cashOut");
            var privateKey = game.Private_key;
            var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(game.Address);
            var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
            var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, contract.Address, new System.Numerics.BigInteger(0), txCount.Value, gasPrice, 90000, cashOutFuntion.GetData(game.Address, player.Address, coins * 10000));

            var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("tx",txId),
                new KeyValuePair<string, string>("receiver", player.Id.ToString()),
                new KeyValuePair<string, string>("sender", game.Id.ToString()),
                new KeyValuePair<string, string>("ammount", coins.ToString()),
                new KeyValuePair<string, string>("type", "cashout")
            });
            var result = await client.PostAsync("addtransaction", content);
            string resultContent = await result.Content.ReadAsStringAsync();
        
            return txId;


        }
        public async Task<String> WinAsync(double coins)
        {
            if (String.IsNullOrEmpty(player.Address))
            {
                throw new UserNotSetException();
            }
            if (String.IsNullOrEmpty(game.Address))
            {
                throw new GameNotSetException();
            }

            var winFunction = contract.GetFunction("win");
            var privateKey = game.Private_key;
            var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(game.Address);
            var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
            var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, contract.Address, new System.Numerics.BigInteger(0), txCount.Value, gasPrice, 90000, winFunction.GetData(game.Address, player.Address, coins * 10000));

            var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("tx",txId),
                new KeyValuePair<string, string>("receiver", player.Id.ToString()),
                new KeyValuePair<string, string>("sender", game.Id.ToString()),
                new KeyValuePair<string, string>("ammount", coins.ToString()),
                new KeyValuePair<string, string>("type", "win")
            });
            var result = await client.PostAsync("addtransaction", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            return txId;


        }
        public async Task<String> loseAsync(double coins)
        {
            if (String.IsNullOrEmpty(player.Address))
            {
                throw new UserNotSetException();
            }
            if (String.IsNullOrEmpty(game.Address))
            {
                throw new GameNotSetException();
            }

            var loseFuntion = contract.GetFunction("lose");
            var privateKey = game.Private_key;
            var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(game.Address);
            var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
            var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, contract.Address, new System.Numerics.BigInteger(0), txCount.Value, gasPrice, 90000, loseFuntion.GetData(game.Address, player.Address, coins * 10000));

            var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("tx",txId),
                new KeyValuePair<string, string>("receiver", player.Id.ToString()),
                new KeyValuePair<string, string>("sender", game.Id.ToString()),
                              new KeyValuePair<string, string>("ammount", coins.ToString()),
                new KeyValuePair<string, string>("type", "lose")
            });
            var result = await client.PostAsync("addtransaction", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            return txId;


        }
        #endregion
        public bool MyRemoteCertificateValidationCallback(System.Object sender,
X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain,
            // look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        continue;
                    }
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                        break;
                    }
                }
            }
            return isOk;
        }
    }

}
