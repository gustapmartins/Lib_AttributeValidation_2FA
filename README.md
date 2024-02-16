# Autenticação de Dois Fatores com Google Authenticator

Este é um exemplo de implementação de autenticação de dois fatores utilizando o Google Authenticator em C#.

## Descrição

Este projeto consiste em uma biblioteca para gerar códigos QR e validar códigos de autenticação de dois fatores utilizando o Google Authenticator. Ele fornece métodos para gerar um código QR contendo a chave secreta e para validar o código inserido pelo usuário.

![Google Authenticator](https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/Google-authenticator-icon.svg/1200px-Google-authenticator-icon.svg.png)

---

## Como Usar

Para começar a utilizar a autenticação de dois fatores em seu aplicativo, siga estas etapas:

1. **Instalação**

    Você pode adicionar esta biblioteca ao seu projeto C# através do NuGet Package Manager. Basta procurar por "Authentication_2FA" e instalar o pacote.

2. **Geração de Chave e Código QR**

    Para um novo usuário, chame o método `GenerateQR` passando o email do usuário como parâmetro. Isso irá gerar uma chave secreta e um código QR contendo as informações necessárias para configurar o Google Authenticator no dispositivo do usuário.

    Exemplo:
    ```csharp
    Authentication auth = new Authentication();
    string qrCodeUrl = auth.GenerateQR("usuario@example.com");
    ```

3. **Validação do Código de Autenticação**

    Quando o usuário tentar fazer login, você precisará validar o código de autenticação inserido por ele. Para isso, chame o método `ValidateCode`, passando o código inserido e a chave secreta do usuário.

    Exemplo:
    ```csharp
    bool isValid = auth.ValidateCode("123456", secretKey);
    ```

## Requisitos

- .NET Framework 4.6.1 ou superior

## Contribuição

Se você quiser contribuir para este projeto, fique à vontade para abrir issues ou enviar pull requests. Toda contribuição é bem-vinda!

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para obter mais detalhes.
