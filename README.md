# .net-console

*A sample console application using the Zapster.Api.Client NuGet package.*

This sample provides some simple code to use the client that will allow you to query the Zapster API over HTTP(S).

The Zapster API endpoint is https://zapster.io/api

## Using the API Client

The NuGet package is located at https://www.nuget.org/packages/Zapster.Api.Client/

### Get Exchange Rate

```bash
using (var client = new ZapsterApiClient(apiEndpoint))
{
	var transaction = client.Exchange.Calculate(accountId, CurrencyCode.CHF, 9.99M);
	......
}
```

### Create Transaction

```bash
using (var client = new ZapsterApiClient(apiEndpoint))
{
	var transaction = client.Transactions.Create(accountId, CurrencyCode.GBP, 9.99M);
	......
}
```

### Get Transaction

```bash
using (var client = new ZapsterApiClient(apiEndpoint))
{
	var transaction = client.Transactions.Get(transactionId);
	......
}
```