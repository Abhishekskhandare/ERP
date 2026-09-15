using System.Security.Cryptography;
using System.Text;

namespace ERP.Helper
{
	public static class AesHelper
	{
		private static readonly byte[] Key = Encoding.UTF8.GetBytes("AbhishekshailendrakhandareAbhish"); // 32 bytes for AES-256
		private static readonly byte[] IV = Encoding.UTF8.GetBytes("1A2B3C4D5E6F7G8H"); // 16 bytes for AES


		public static string EncryptString(string plainText)
		{
			using (var aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;

				using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					using (var sw = new StreamWriter(cs))
					{
						sw.Write(plainText);
					}
					return Convert.ToBase64String(ms.ToArray());
				}
			}

		}

		public static string DecryptString(string cipherText)
		{
			using (var aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;

				using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
				using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
				using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
				using (var sr = new StreamReader(cs))
				{
					return sr.ReadToEnd();
				}
			}
		}
	}

}



