# IdP-demo

A simple demo for an Identity Provider service implementing IdentityServer4

Read the article [here](https://engineering.hexacta.com)

## Solution projects

- IdPServer: the Identity Provider service
- Client: a console application requesting tokens from the IdP with the Client Credentials flow
- WebClient: a web application requesting tokens from the IdP with the Authorization Code flow
- SecuredApi: a web API with endpoints requiring authentication using the JWTs issued by the IdPServer
