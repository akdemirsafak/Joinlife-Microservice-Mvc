﻿using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Joinlife.webui.Settings;
using Microsoft.Extensions.Options;

namespace Joinlife.webui.AuthOps;


public class ClientCredentialTokenService : IClientCredentialTokenService
{
    //Token'ı memory'e kaydedeceğiz.Bu kısımda IdentityModel.AspNetCore'i nuget üzerinden yükleyelim.
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly ClientSettings _clientSettings;
    private readonly IClientAccessTokenCache _clientAccessTokenCache; //IdentityModel token management'den gelir.
    private readonly HttpClient _httpClient;
    public ClientCredentialTokenService(
        IOptions<ServiceApiSettings> serviceApiSettings,
        IOptions<ClientSettings> clientSettings,
        IClientAccessTokenCache clientAccessTokenCache,
        HttpClient httpClient)
    {
        _serviceApiSettings = serviceApiSettings.Value;
        _clientSettings = clientSettings.Value;
        _clientAccessTokenCache = clientAccessTokenCache;
        _httpClient = httpClient;
    }

    public async Task<string> GetToken()
    {

        var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken", new ClientAccessTokenParameters());
        if (currentToken is not null)
        {
            return currentToken.AccessToken;
        }
        //IdentityService'deki işlemin benzeri
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy { RequireHttps = true }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception;
        }
        var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _clientSettings.WebClient.ClientId,
            ClientSecret = _clientSettings.WebClient.ClientSecret,
            Address = discovery.TokenEndpoint
        };
        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
        if (newToken.IsError)
        {
            throw newToken.Exception;
        }
        await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn, new ClientAccessTokenParameters());
        return newToken.AccessToken;
    }
}
