using Encryption;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Dal.Encryption
{
    public class EncryptDecryptUtil
    {
        string _encryptType = System.Configuration.ConfigurationManager.AppSettings["encrypttype"].ToString().ToLower();
        public enum EncryptType
        {
            Symmetric = 0,
            Asymmetric = 1,
            Hash = 2
        }
        /// <summary>
        /// Sql parameter array with encrypt values
        /// </summary>
        /// <param name="fields">The fields list which needs the encryption</param>
        /// <param name="ht">Collection of all parameters</param>
        /// <returns>sql parameter array</returns>
        internal SqlParameter[] GetSqlParamterArray(List<string> fields, System.Collections.Hashtable ht)
        {
            var sqlParams = new List<SqlParameter>();
            try
            {
                var en = ht.GetEnumerator();
                while (en.MoveNext())
                {
                    string paramtername = en.Key.ToString();
                    string paramtervalue = string.Empty;
                    var x = new List<string>(fields);
                    if (x.Contains(paramtername))
                    {
                        paramtervalue = EncryptData(en.Value.ToString(), GetEncryptType());
                    }
                    else
                    {
                        if (en.Value != null)
                            paramtervalue = en.Value.ToString();
                        else
                            paramtervalue = "";
                    }
                    var oParam = new SqlParameter(paramtername, paramtervalue);
                    sqlParams.Add(oParam);
                }
            }
            catch (Exception ex)
            {
                string y = ex.ToString();
            }
            return sqlParams.ToArray();
        }
        public string GetDecryptData(string x)
        {
            return DecryptData(x, GetEncryptType());
        }
        public EncryptType GetEncryptType()
        {
            switch (_encryptType)
            {
                case "sym":
                    return EncryptType.Symmetric;
                case "asym":
                    return EncryptType.Asymmetric;
                case "hash":
                    return EncryptType.Hash;
                default:
                    return EncryptType.Symmetric;
            }
        }
        public EncryptType GetEncryptType(string type)
        {
            switch (type)
            {
                case "sym":
                    return EncryptType.Symmetric;
                case "asym":
                    return EncryptType.Asymmetric;
                case "hash":
                    return EncryptType.Hash;
                default:
                    return EncryptType.Symmetric;
            }
        }
        public string EncryptData(string data, EncryptType encryptType)
        {
            string encryptedString = "";
            if (data == "null")
            {
                encryptedString = "null";
            }
            if (data != null && data != string.Empty && data.ToLower() != "null")
            {
                switch (encryptType)
                {
                    case EncryptType.Symmetric:
                        Symmetric.Provider p = Symmetric.Provider.TripleDES;
                        Symmetric sym = new Symmetric(p);
                        //sym.Key.Text = symmetricKey;
                        Data encryptedData = null;
                        encryptedData = sym.Encrypt(new Data(data));
                        encryptedString = encryptedData.Base64;
                        break;
                    case EncryptType.Asymmetric:
                        Asymmetric asym = new Asymmetric();
                        Asymmetric.PublicKey pubkey = new Asymmetric.PublicKey();
                        Asymmetric.PrivateKey privkey = new Asymmetric.PrivateKey();
                        asym.GenerateNewKeyset(ref pubkey, ref privkey);
                        Data EncryptedData = null;
                        EncryptedData = asym.Encrypt(new Data(data), pubkey);
                        encryptedString = EncryptedData.Base64;
                        break;
                    case EncryptType.Hash:
                        Hash.Provider phash = Hash.Provider.SHA1;
                        Hash hash = new Hash(phash);
                        EncryptedData = hash.Calculate(new Data(data));
                        encryptedString = EncryptedData.Base64;
                        break;
                }
            }
            return encryptedString;
        }
        public string DecryptData(string encryptedString, EncryptType encryptType)
        {
            string data = "";
            if (encryptedString != null && encryptedString != string.Empty && data.ToLower() != "null")
            {
                switch (encryptType)
                {
                    case EncryptType.Symmetric:
                        Symmetric.Provider p = Symmetric.Provider.TripleDES;
                        Data symdecryptedData = null;
                        p = Symmetric.Provider.TripleDES;
                        Symmetric sym2 = new Symmetric(p);
                        //sym2.Key.Text = symmetricKey;
                        System.Text.ASCIIEncoding symEncoding = new System.Text.ASCIIEncoding();
                        Byte[] symBytes = symEncoding.GetBytes(encryptedString);
                        Data symencryptedData = null;
                        symencryptedData = new Data();
                        symencryptedData.Base64 = encryptedString;
                        symdecryptedData = sym2.Decrypt(symencryptedData);
                        data = symdecryptedData.Text;
                        break;
                    case EncryptType.Asymmetric:
                        System.Text.ASCIIEncoding asymEncoding = new System.Text.ASCIIEncoding();
                        Data asymdecryptedData = null;
                        Asymmetric asym2 = new Asymmetric();
                        Byte[] asymBytes = asymEncoding.GetBytes(encryptedString);
                        Data asymencryptedData = null;
                        asymencryptedData = new Data();
                        asymencryptedData.Base64 = encryptedString;
                        asymdecryptedData = asym2.Decrypt(asymencryptedData);
                        data = asymdecryptedData.Text;
                        break;
                    case EncryptType.Hash:
                        //hashing is one way
                        break;
                }
            }
            return data;
        }
    }
}
