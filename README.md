
![https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png](https://yoonik.me/wp-content/uploads/2019/08/cropped-LogoV4_TRANSPARENT.png)

# YooniK Services Client DotNet SDK

[![License](https://img.shields.io/pypi/l/yk_face.svg)](https://github.com/dev-yoonik/YK-Services-Client-DotNetCore/blob/master/LICENSE)

This repository contains the Services Client for an easier communication experience when using YooniK APIs SDKs, an [YooniK Services](https://yoonik.me) offering.

For more information please [contact us](mailto:info@yoonik.me).

## Getting started

To import the latest this solution into your project, enter the following command in the NuGet Package Manager Console in Visual Studio:

For other installation methods, see [YooniK Services Client Nuget](https://www.nuget.org/packages/YooniK.Services.Client/)

```
PM> Install-Package YooniK.Services.Client
```

Use it:

```csharp

// Example data
string faceApiBaseUrl = "http://127.0.0.1:5001/v1.1/yoonik/";
string faceApiSubscriptionKey = "7390ef95-770b-485d-a30c-3fa2ab66f131";

// instantiate an IConnectionInformation with the above information
IConnectionInformation faceApiConnectionInformation = new ConnectionInformation(faceApiBaseUrl, faceApiSubscriptionKey);

// instantiate an IServiceClient and pass the IConnectionInformation
IServiceClient faceClient = new ServiceClient(faceApiConnectionInformation);

/* 
    To use the Request methods its needed an IRequestMessage instantiated object.
    Its required to specified the HttpMethod parameter.
    This allows for a custom HTTP request creation, from custom headers, query string, URL relative path, and an IRequest object.  
*/
IRequestMessage requestMessage = new RequestMessage(System.Net.Http.HttpMethod.Get);

// !!! NOTE Reponse Content in string, use the following
await faceClient.RequestAsync(requestMessage);

// Reponse Content in an deserializable object use
await faceClient.RequestAsync<DeserializableObjectType>(requestMessage);

```
