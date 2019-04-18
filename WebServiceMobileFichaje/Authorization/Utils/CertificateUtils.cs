using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Xml;

namespace WebServiceMobileFichaje.Authorization.Utils
{
    public class CertificateUtils
    {
        public static RsaSecurityKey GetRSAKey()
        {
            RsaSecurityKey key;
            var path = System.Web.HttpContext.Current.Request.MapPath("~/Authorization/Resources/private.xml");

            using (var textReader = new System.IO.StreamReader(System.IO.File.OpenRead(path)))
            {
                RSA rsa = RSA.Create();
                key = new RsaSecurityKey((FromXmlString(textReader.ReadToEnd())));
            }

            return key;
        }
        public static RSAParameters FromXmlString(string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            return parameters;
        }
    }
}