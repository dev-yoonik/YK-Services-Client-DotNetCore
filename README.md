
![https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png](https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png)

# YooniK Services Client DotNet SDK

[![Nuget](https://img.shields.io/nuget/v/YooniK.Services.Client)](https://www.nuget.org/packages/YooniK.Services.Client/)
[![License](https://img.shields.io/pypi/l/yk_face.svg)](https://github.com/dev-yoonik/YK-Services-Client-DotNetCore/blob/master/LICENSE)

This repository contains the Services Client for an easier communication experience when using YooniK APIs SDKs, an [YooniK Services](https://yoonik.me) offering.

For more information please [contact us](mailto:info@yoonik.me).

## Getting started

To import the latest this solution into your project, enter the following command in the NuGet Package Manager Console in Visual Studio:

For other installation methods, see [YooniK Services Client Nuget](https://www.nuget.org/packages/YooniK.Services.Client/)

```
PM> Install-Package YooniK.Services.Client
```

## Example

Use it:

```csharp

// Example data
string apiBaseUrl = "YOUR-API-ENDPOINT";
string apiSubscriptionKey = "YOUR-X-API-KEY-ENDPOINT";

// instantiate an IConnectionInformation with the above information
var apiConnectionInformation = new ConnectionInformation(apiBaseUrl, apiSubscriptionKey);

// instantiate an IServiceClient and pass the IConnectionInformation
var apiClient = new ServiceClient(apiConnectionInformation);

/* 
    To use the Request methods its needed an IRequestMessage instantiated object.
    Its required to specified the HttpMethod parameter.
    This allows for a custom HTTP request creation, from custom headers, query string, URL relative path, and an IRequest object.  
*/
var requestMessage = new RequestMessage(System.Net.Http.HttpMethod.Get);

// !!! NOTE Response Content in string, use the following
await apiClient.RequestAsync(requestMessage);

// Response Content in an deserializable object use
await apiClient.RequestAsync<DeserializableObjectType>(requestMessage);

```
