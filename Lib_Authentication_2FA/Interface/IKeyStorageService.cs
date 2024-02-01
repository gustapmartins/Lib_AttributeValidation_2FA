namespace Lib_Authentication_2FA.Interface;

public interface IKeyStorageService
{
    void SaveKey(string email, string key);

    string GetKey(string email);
}
