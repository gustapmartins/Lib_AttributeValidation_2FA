using Lib_Authentication_2FA;

namespace Test;

public class TestAuthentication
{
    [Fact]
    public void GenerateQR_ValidEmail_ReturnsQRCodeUrl()
    {
        //Arrange
        var authentication2FA = new Authentication_2FA();
        var email = "user@example.com";
    
        //Act
        var qrCodeUrl = authentication2FA.GenerateQR(email);
    
        //Assert
        Assert.NotNull(qrCodeUrl);
        Assert.StartsWith("data:image/png;base64,", qrCodeUrl);
    }

    [Fact]
    public void GenerateQR_NullEmail_ThrowsArgumentException()
    {
        // Arrange
        var authentication2FA = new Authentication_2FA();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => authentication2FA.GenerateQR(null));
    }

    [Fact]
    public void ValidateCode_ValidCodeAndKey_ReturnsTrue()
    {
        // Arrange
        var authentication2FA = new Authentication_2FA();
        var code = "123456"; // Substitua com um código válido
        var key = "ABCDEF123456"; // Substitua com uma chave válida

        // Act
        var result = authentication2FA.ValidateCode(code, key);

        // Assert
        Assert.True(result, $"Falha na validação do código '{code}' para a chave '{key}'.");
    }
}