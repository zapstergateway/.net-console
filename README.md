# .net-console

*A sample console application using the Zapster.Api.Client NuGet package.*

This sample provides some simple code to use the client that will allow you to query the Zapster API over HTTP(S).

The Zapster API endpint is https://zapster.io/api

The NuGet package is located at https://www.nuget.org/packages/Zapster.Api.Client/

## Using the API Client

```bash
using (var client = new ZapsterApiClient(apiEndpoint))
{
	var transaction = client.Transactions.Create(account, CurrencyCode.USD, 9.99M);
	......
}
```
