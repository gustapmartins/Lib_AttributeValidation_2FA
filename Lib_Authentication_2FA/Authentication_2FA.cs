using Google.Authenticator;
using Lib_Authentication_2FA.Interface;
using System.Security.Cryptography;

namespace Lib_Authentication_2FA;

public class Authentication_2FA
{
    private readonly TwoFactorAuthenticator _tfa;
    private readonly IKeyStorageService _keyStorageService;

    public Authentication_2FA(IKeyStorageService keyStorageService)
    {
        _tfa = new TwoFactorAuthenticator();
        _keyStorageService = keyStorageService ?? throw new ArgumentNullException(nameof(keyStorageService));
    }

    /// <summary>
    /// Gera uma chave secreta para autenticação de dois fatores.
    /// </summary>
    /// <returns>A chave secreta deve ser gerada.</returns>
    private string GenerateSecretKey(int keySize)
    {
        if (keySize <= 0)
        {
            throw new ArgumentException("O tamanho da chave deve ser maior que zero.");
        }

        var generateNumber = RandomNumberGenerator.Create();

        byte[] randomBytes = new byte[keySize];
        generateNumber.GetBytes(randomBytes);

        return BitConverter.ToString(randomBytes).Replace("-", string.Empty);
    }

    /// <summary>
    /// Gera um código QR e salva a chave no banco de dados para um usuário.
    /// </summary>
    /// <param name="email">O email do usuário.</param>
    /// <param name="keySize">Defina o tamanho da chave gerada </param>
    /// <param name="codeDurationSeconds"></param>
    /// <param name="issuer">O emissor da chave (padrão é "MyApp").</param>
    /// <returns>O URL do código QR gerado.</returns>
    public string GenerateQR(string email, int keySize = 10, string issuer = "MyApp", int codeDurationSeconds = 3)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("O email do usuário não pode ser nulo ou vazio.");
            }

            string key = GenerateSecretKey(keySize);

            // Salvar a chave associada ao usuário
            _keyStorageService.SaveKey(email, key);

            SetupCode setupInfo = _tfa.GenerateSetupCode(issuer, email, key, false, codeDurationSeconds);

            return setupInfo.QrCodeSetupImageUrl;
        }
        catch (Exception ex)
        {
            // Trate a exceção ou registre-a, dependendo dos requisitos do seu aplicativo.
            throw new ArgumentException($"Erro ao gerar código QR: {ex.Message}");
        }
    }

    /// <summary>
    /// Valida um código de autenticação de dois fatores.
    /// </summary>
    /// <param name="key">Chave atrelado ao usuario</param>
    /// <param name="code">O código inserido pelo usuário.</param>
    /// <returns>True se o código for válido, False caso contrário.</returns>
    public bool ValidateCode(string code, string key)
    {
        try
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("O código e a chave não podem ser nulos ou vazios.");
            }

            return _tfa.ValidateTwoFactorPIN(key, code);
        }
        catch (Exception ex)
        {
            // Logar a exceção ou tratar conforme necessário.
            Console.WriteLine($"Erro ao validar código de autenticação: {ex.Message}");
            return false;
        }
    }
}
