﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace reCaptcha
{
    
    #line 3 "..\..\ReCaptcha.cshtml"
    using System;
    
    #line default
    #line hidden
    
    #line 4 "..\..\ReCaptcha.cshtml"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    
    #line 5 "..\..\ReCaptcha.cshtml"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 6 "..\..\ReCaptcha.cshtml"
    using System.IO;
    
    #line default
    #line hidden
    
    #line 7 "..\..\ReCaptcha.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    
    #line 8 "..\..\ReCaptcha.cshtml"
    using System.Net;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 9 "..\..\ReCaptcha.cshtml"
    using System.Web;
    
    #line default
    #line hidden
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using System.Web.WebPages.Html;
    
    #line 10 "..\..\ReCaptcha.cshtml"
    using JetBrains.Annotations;
    
    #line default
    #line hidden
    
    #line 11 "..\..\ReCaptcha.cshtml"
    using Newtonsoft.Json;
    
    #line default
    #line hidden
    
    #line 12 "..\..\ReCaptcha.cshtml"
    using Newtonsoft.Json.Linq;
    
    #line default
    #line hidden
    
    #line 13 "..\..\ReCaptcha.cshtml"
    using reCaptcha;
    
    #line default
    #line hidden
    
    #line 14 "..\..\ReCaptcha.cshtml"
    using reCaptcha.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class ReCaptcha : System.Web.WebPages.HelperPage
    {
        
        #line 18 "..\..\ReCaptcha.cshtml"

    private const string reCaptchaVerifyUrl = "https://www.google.com/recaptcha/api/siteverify";

    private static readonly object ErrorCodesCacheKey = new object();
    
    internal static string GetValidationUrl(HttpContextBase context, string privateKey)
    {
        var remoteIp = context.Request.ServerVariables["REMOTE_ADDR"];
        if (String.IsNullOrEmpty(remoteIp))
        {
            throw new InvalidOperationException("reCaptcha remote IP not found");
        }

        // get user's response
        var response = (context.Request.Form["g-recaptcha-response"] ?? String.Empty).Trim();

        //
        var builder = new UriBuilder(reCaptchaVerifyUrl);
        var querystring = new NameValueCollection()
            {
                { "secret", privateKey},
                { "response", response},
                { "remoteip", remoteIp},
            };
        builder.Query = querystring.ToQueryString(false);

        return builder.ToString();
    }

    internal static string GetValidationResponse(string requestUrl)
    {
        var request = WebRequest.Create(requestUrl);

        using (var response = request.GetResponse())
        {
            var stream = response.GetResponseStream();

            if (stream == null)
                return null;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public static bool Validate([NotNull] string privateKey)
    {
        return Validate(HttpContext.Current == null ? null : new HttpContextWrapper(HttpContext.Current), privateKey);
    }


    internal static bool Validate(HttpContextBase context, [NotNull] string privateKey)
    {
        // get recaptcha validation api url
        var reqUrl = GetValidationUrl(context, privateKey);

        var response = GetValidationResponse(reqUrl);

        // no response - return false
        if (response == null)
            return false;

        // TODO: bad response will throw exception
        var responseJson = JsonConvert.DeserializeObject<dynamic>(response);

        if (responseJson == null)
            return false;

        // use the error-codes returned

        JArray jArr = responseJson["error-codes"];

        if (jArr != null)
        {
            var errorCodes = jArr.Select(e => e.ToString()).ToArray();

            var codesEnum = errorCodes.Select(EnumHelper<ErrorCodes>.GetValueFromDescription);

            // save errors
            SetLastErrors(context, codesEnum);
        }

        return responseJson.success;
    }

    internal static void SetLastErrors(HttpContextBase context, IEnumerable<ErrorCodes> value)
    {
        context.Items[ErrorCodesCacheKey] = value;
    }

    public static IEnumerable<ErrorCodes> GetLastErrors(HttpContextBase context)
    {
        context = context ?? (HttpContext.Current != null ? new HttpContextWrapper(HttpContext.Current) : null);

        if (context == null)
            return null;
            
        if (context.Items.Contains(ErrorCodesCacheKey))
        {
            return context.Items[ErrorCodesCacheKey] as IEnumerable<ErrorCodes>;
        }
        return null;
    }

    public static IEnumerable<string> GetLastErrorsNames(HttpContextBase context)
    {
        return GetLastErrors(context).Select(e => Enum.GetName(typeof (ErrorCodes), e));
    }


        #line default
        #line hidden

public static System.Web.WebPages.HelperResult GetHtml(string publicKey = null, string theme = null, string type = null, string callback = null)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 132 "..\..\ReCaptcha.cshtml"
 
    // Ms helper - old
    //ReCaptcha.GetHtml(Context, @publicKey)

    // explicit call to recaptcha
    
#line default
#line hidden



#line 149 "..\..\ReCaptcha.cshtml"
      

    if (String.IsNullOrEmpty(publicKey))
    {
        throw new ArgumentException("can't be null", "publicKey");
    }


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <script src=\"https://www.google.com/recaptcha/api.js\" async defer></script>\r\n" +
"");



WriteLiteralTo(@__razor_helper_writer, "    <div class=\"g-recaptcha\" data-sitekey=\"");



#line 157 "..\..\ReCaptcha.cshtml"
            WriteTo(@__razor_helper_writer, publicKey);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" data-theme=\"");



#line 157 "..\..\ReCaptcha.cshtml"
                                    WriteTo(@__razor_helper_writer, theme);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" data-type=\"");



#line 157 "..\..\ReCaptcha.cshtml"
                                                       WriteTo(@__razor_helper_writer, type);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" data-callback=\"");



#line 157 "..\..\ReCaptcha.cshtml"
                                                                             WriteTo(@__razor_helper_writer, callback);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\"></div>\r\n");



#line 158 "..\..\ReCaptcha.cshtml"

#line default
#line hidden

});

}


        public ReCaptcha()
        {
        }
    }
}
#pragma warning restore 1591
