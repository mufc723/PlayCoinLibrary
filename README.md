# PlayCoin Library

Library to interact with playcoin wallet in unity games .

## Getting Started

These instructions will walk you through using playcoin library in your unity game .

### Prerequisites

Things you'll be needing
```
Game Developer Account at https://www.playcoin.io/
Unity supporting api level .NET 4.6 or above  . 
```
see here for upgrading [Unity Scripting Api](https://docs.unity3d.com/Manual/ScriptingRuntimeUpgrade.html)

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be

```
PM> Install-Package PlayCoinLibrary -Version 1.0.0	

```

Choose PlayCoinLibrary 

### Usage

Initialize the playcoin object
```csharp
 PlayCoin playCoin = new PlayCoin();
```

Sign in with your developer account and player account .

```csharp
bool gameLogged = false;
bool playerLogged = false;
  var task = Task.Run(async () =>
        {
            gameLogged = await playCoin.SignGame("gameUsername", "gamePassword"));
            playerLogged = await playCoin.SignPlayer("playerUsername", "playerPassword"));
        });
   task.Wait();
```
Set your game unit in the blockchain.
```csharp
String tx = "";
  var task = Task.Run(async () =>
        {
            tx = await playCoin.SetGameUnitAsync(2)); //your in-game coin is worth 2 playcoins
        });
   task.Wait();
```
Finally , use the available methods for example :
```csharp
String tx = "";
  var task = Task.Run(async () =>
        {
            tx = await playCoin.TopUpAsync(99)); //your in-game coin is worth 2 playcoins
        });
   task.Wait();
```
#### Available Methods
*  ``` bool SignGame(String username,String password) ```
*  ``` bool SignPlayer(String username,String password) ```
*  ``` int GetGameUnitAsync() ```
*  ``` String SetGameUnitAsync(int value) ```
*  ``` String TopUpAsync(int coins) ```
*  ``` String CashoutAsync(int coins) ```
*  ``` String WinAsync(int coins) ```
*  ``` String LoseAsync(int coins) ```

## Built With

* [Nethereum](https://nethereum.readthedocs.io/en/latest/) - Web3 Library


## Authors

* **Achref Gharbi** - *Developer* - 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Finally

* One coin changes the way we use financial services

