# adb2c-custom-claims

testing out custom claims and policy in azure ad b2c

## Links used for Research

* [Get started with custom policies in Azure Active Directory B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-get-started?tabs=applications)
* [Set up sign-in with an Azure Active Directory account using custom policies in Azure Active Directory B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/identity-provider-azure-ad-single-tenant-custom?tabs=applications)
* [Integrate REST API claims exchanges in your Azure AD B2C user journey as validation of user input](https://docs.microsoft.com/en-us/azure/active-directory-b2c/rest-api-claims-exchange-dotnet)
* [Add REST API claims exchanges to custom policies in Azure Active Directory B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-rest-api-claims-exchange)
* [Azure Active Directory B2C: Use custom attributes in a custom profile edit policy](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-custom-attributes)
* [Publish an ASP.NET Core app to Azure with Visual Studio Code](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vscode?view=aspnetcore-3.1)

## Other Research to Consider

* [Define a RESTful technical profile in an Azure Active Directory B2C custom policy](https://docs.microsoft.com/en-us/azure/active-directory-b2c/restful-technical-profile)
* [Secure RESTful APIs with basic auth](https://docs.microsoft.com/en-us/azure/active-directory-b2c/secure-rest-api-dotnet-basic-auth)
* [Secure RESTful APIs with certificate auth](https://docs.microsoft.com/en-us/azure/active-directory-b2c/secure-rest-api-dotnet-certificate-auth)

## AADB2C API

Before completing this section be sure to follow all of the steps in [Get started with custom policies in Azure Active Directory B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-get-started?tabs=applications) including all [Prerequisites](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-get-started?tabs=applications#prerequisites) mentioned on that page.

Example API found in source code [Contoso.AADB2C.API](src/Contoso.AADB2C.API)

Ensure the project will build by cd to this directory and run the following:

```powershell
dotnet build
```

To provision an Azure App Service resource to host the API in Azure:

* Ensure you have [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) installed
* Update the [aadb2c.api.params.json](ops/aadb2c.api.params.json) file with the values for your environment
* Run the command below (replace -g parameter with your resource group name)

    ```powershell
    az group deployment create `
    -g <your resource group name> `
    --template-file ops\aadb2c.api.azuredeploy.json `
    --parameters @ops\aadb2c.api.params.json
    ```

* In the directory of the API project, prepare the project to be published

    ```powershell
    dotnet publish -c Release -o ./publish
    ```

* In VS Code follow these steps:
  * Right click the publish folder and select Deploy to Web App...
  * Select the subscription the existing Web App resides
  * Select the Web App from the list
  * Visual Studio Code will ask you if you want to overwrite the existing content. Click Deploy to confirm
