using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// Testing the REST service:
// 1.  Open this file. Good, you're already here.
// 2.  Click Debug -> Start Debugging (F5)
// 3.  This will open up Google Chrome to the default localhost path. 
// 4.  Click the AuthenticationService.svc file name. 
// 5.  Copy the URL. It should be something like: (http://localhost:64610/AuthenticationService.svc). 
// 6.  Open up Fiddler2. 
// 7.  Open the Composer tab. 
// 8.  Under the Parsed tab, select POST from the dropdown (use HTTP/1.1)
// 9.  Paste the aforementioned URL in the POST URL path.
// 10. In the Request Headers text box, enter the following data:
//     User-Agent: Fiddler
//     Content-Type: application/json; charset=utf-8
// 11. (NOTE: Make sure you remove the comment lines from the above two lines)
// 12. In the Request Body text box, copy and paste the following JSON:
//     {"request":{"GlobalizationHeader":{"Locale":"en-US", "Tz":"UTC", "Options":[{"Key":"abc", "Value":"123"}]},"EmailAddress":"test@test.com","Password":"xyzabc"}}
// 13. In Visual Studio, set a breakpoint on the AuthenticationService.svc.cs New User registration line (public AuthenticationResponse NewUserRegistration(NewUserRegistrationRequest request))
// 14. Click Execute in Fiddler. 
// 15. You should hit the breakpoint and you should see the data populated from the JSON request. 
// 16. If Fiddler returns a RED row, you got an error.

namespace Mystic.Service.REST
{
    
    [DataContract]
    public class KeyValuePair
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

    /// <summary>
    /// A GlobalizationHeader is passed with all requests to the REST service and is used to lookup
    /// localizable data for the requesting client: Error messages, strings, etc. By default,
    /// if the GlobalizationHeader does not exist or does not provide a Locale or TimeZone,
    /// they will be defaulted to en-US and UTC, respectively.
    /// </summary>
    [DataContract]
    public class GlobalizationHeader
    {
        [DataMember]
        public string Locale { get; set; }

        [DataMember]
        public string TimeZone { get; set; }

        [DataMember]
        public List<KeyValuePair> Options { get; set; }
    }

    [DataContract]
    public class AuthenticationRequest
    {
        [DataMember]
        public GlobalizationHeader GlobalizationHeader { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string Password { get; set; }
    }

    /// <summary>
    /// The contract provided back to the client on an authentication request.
    /// </summary>
    [DataContract]
    public class AuthenticationResponse
    {
        [DataContract]
        public enum StatusMessage
        {
            [EnumMember(Value = "Ok")]
            Ok,
            [EnumMember(Value = "Fail")]
            Fail
        }

        [DataMember]
        private string StatusString { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string MessageDetails { get; set; }

        /// <summary>
        ///  Ugly hack to get around a JSON-serializaton parsing issue with Enums in which
        ///  the integer value of the enum is always passed back on the request. That's fine,
        ///  but it makes sense to refer by status enum, here.
        /// </summary>
        public StatusMessage Status
        {
            get { return (StatusMessage)Enum.Parse(typeof(StatusMessage), StatusString); }
            set { StatusString = value.ToString(); }
        }

    }

    [DataContract]
    public class NewUserRegistrationRequest
    {
        // {"request":{"GlobalizationHeader":{"Locale":"en-US", "Tz":"PST", "Options":[{"Key":"abc", "Value":"123"}]},"EmailAddress":"jgshort@openiep.com","Password":"xyzabc"}}

        [DataMember]
        public GlobalizationHeader GlobalizationHeader { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string Password { get; set; }
    }

    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract, WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Authenticate/Verify")]
        AuthenticationResponse Authenticate(AuthenticationRequest request);

        [OperationContract, WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Authenticate/Register")]
        AuthenticationResponse NewUserRegistration(NewUserRegistrationRequest request);
    }
}
