using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Security.Cryptography;

using Mystic.Common.DTO;
using Mystic.DAL;

namespace Mystic.Service.REST
{
    public class AuthenticationService : IAuthenticationService
    {
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 256;
        private const int MaxEmailAddressLength = 256;
        private const int MinEmailAddressLength = 3;

        private const int EncryptedSaltLength = 128;
        private const int EncryptionIterations = 1024;
        private const int EncryptedHashLength = 128;

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            // TODO: Flesh this out. This is a dummy response.
            return new AuthenticationResponse { Status = AuthenticationResponse.StatusMessage.Ok, Message = string.Format("You entered: {0}", request.EmailAddress) };
        }

        // This can be tested by:
        // 1. Open the IAuthenticationService.cs file in this window. Follow the instructions listed there.
        public AuthenticationResponse NewUserRegistration(NewUserRegistrationRequest request)
        {
            var credential = DAL.Repositories.CredentialRepository.GetBy(1);
            try
            {
                if (string.IsNullOrEmpty(request.EmailAddress) || (request.EmailAddress.Length > MaxEmailAddressLength || request.EmailAddress.Length < MinEmailAddressLength))
                {
                    return new AuthenticationResponse
                    {
                        Status = AuthenticationResponse.StatusMessage.Fail,
                        Message = "You must supply a valid email address.",
                        MessageDetails = string.Format("An email address must be at least {0} characters long and no more than {1} characters long.", MinEmailAddressLength, MaxEmailAddressLength)
                    };
                }
                if (string.IsNullOrEmpty(request.Password) || (request.Password.Length < MinPasswordLength || request.Password.Length > MaxPasswordLength) || request.Password.Contains(request.EmailAddress))
                {
                    return new AuthenticationResponse
                    {
                        Status = AuthenticationResponse.StatusMessage.Fail,
                        Message = "You must supply a valid password.",
                        MessageDetails = string.Format("Passwords must be at least {0} characters long and no more than {1} characters long.", MinPasswordLength, MaxPasswordLength)
                    };
                }

                var salt = GenerateSalt(EncryptedSaltLength);
                var saltedPassword = CalculateSha1(request.Password, salt, EncryptionIterations, EncryptedHashLength);

                return new AuthenticationResponse
                {
                    Status = AuthenticationResponse.StatusMessage.Ok,
                    Message = string.Format("New You entered: {0}", request.EmailAddress)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        private byte[] GenerateSalt(int size)
        {
            byte[] data = new byte[size];
            System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            provider.GetBytes(data);
            return data;
        }

        private static byte[] CalculateSha1(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
