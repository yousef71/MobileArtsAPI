using MobileArts.api.Domain.Models;
using MobileArts.api.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileArts.api.Persistence.Utilities
{
    public class MessageUtility : IMessageService

    {
        private readonly MessageConfiguration _SMSConfiguration;

        public MessageUtility(IConfiguration Configuration)
        {
            _SMSConfiguration = Configuration.GetSection("SMSConfigurations").Get<MessageConfiguration>();
        }
        public string Send(string MobileNbr, string Message, string lang)
        {
            string ReturnMessage = "";


            string HTTPWebRespStr = "";
 
                try
                {
                    
                    HttpWebRequest HttpWReq;
                    HttpWebResponse HttpWResp;
                    Stream streamResponse;
                    StreamReader streamRead;


                    string HTTPWebURL = "http://51.210.118.93:8080/websmpp/websms?user={0}&pass={1}&sid={2}&mno={3}&type={5}&text={4}&respformat=json";
                    string _lang;
                if (lang == "1")
                    _lang = "1";
                else
                    _lang = "4";

                    string strPushUrl = string.Format(HTTPWebURL, _SMSConfiguration.user, _SMSConfiguration.pass,
                        _SMSConfiguration.sid, MobileNbr, Message, _lang);
                    HttpWReq = (HttpWebRequest)WebRequest.Create(strPushUrl);
                    HttpWReq.Method = WebRequestMethods.Http.Get;
                //HttpWReq.Timeout = 10;
                    HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                    if (HttpWReq.HaveResponse)
                    {
                        streamResponse = HttpWResp.GetResponseStream();
                        streamRead = new StreamReader(streamResponse);
                        HTTPWebRespStr = System.Convert.ToString(streamRead.ReadToEnd());
                        streamResponse.Close();
                        streamRead.Close();
                        HttpWResp.Close();

                        try
                        {

                            ReturnMessage = HTTPWebRespStr;
                            //if (HTTPWebRespStr.Contains("Error"))
                            //{ 
                            //ReturnMessage = "Error on response string";
                            //}
                            //else
                            //{
                                
                            //    ReturnMessage = "No error"; 
                            //}
                        }
                 

                        catch (Exception ex)
                        {
                            ReturnMessage = $"error: {ex.Message}"; 
                        }
                    }
                    else
                    {
                        ReturnMessage = "Submission no response"; 
                    }
                }
                catch (Exception ex)
                { 
                    ReturnMessage = ex.Message; 
                }
           

            return ReturnMessage;
        }

    }
}
