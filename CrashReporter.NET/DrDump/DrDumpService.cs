﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using CrashReporterDotNET.com.drdump;
using Microsoft.Win32;

namespace CrashReporterDotNET.DrDump
{
    internal class DrDumpService
    {
        public delegate void SendRequestCompletedEventHandler(object sender, SendRequestCompletedEventArgs args);

        public event SendRequestCompletedEventHandler SendRequestCompleted;

        private SendRequestState _sendRequestState;

        private readonly HttpsCrashReporterReportUploader _uploader;

        public DrDumpService()
        {
            _uploader = new HttpsCrashReporterReportUploader();
            var configOverride =
                Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Idol Software\DumpUploader",
                    "ServiceURL", null) as string;
            if (!string.IsNullOrEmpty(configOverride))
            {
                var t = new Uri(configOverride);
                var newUrl = new UriBuilder(_uploader.Url)
                {
                    Scheme = t.Scheme,
                    Host = t.Host,
                    Port = t.Port
                };
                _uploader.Url = newUrl.ToString();
            }
        }

        public void SendAnonymousReportAsync(Exception exception, string toEmail, Guid? applicationId)
        {
            _sendRequestState = new SendRequestState
            {
                AnonymousData = new AnonymousData
                {
                    Exception = exception,
                    ToEmail = toEmail,
                    ApplicationId = applicationId
                }
            };

            _uploader.SendAnonymousReportCompleted += OnSendAnonymousReportCompleted;
            _uploader.SendAnonymousReportAsync(SendRequestState.GetClientLib(), _sendRequestState.GetApplication(),
                _sendRequestState.GetExceptionDescription(true), _sendRequestState);
        }

        public void SendAdditionalDataAsync(Control control, string developerMessage, string userEmail,
            string userMessage, byte[] screenshot)
        {
            bool needToSend;
            lock (_sendRequestState)
            {
                _sendRequestState.PrivateData = new PrivateData
                {
                    UserEmail = userEmail,
                    UserMessage = userMessage,
                    DeveloperMessage = developerMessage,
                    Screenshot = screenshot
                };

                needToSend = _sendRequestState.SendAnonymousReportResult != null;
            }

            if (needToSend)
                SendAdditionalDataAsync(control, _sendRequestState);
        }

        private void SendAdditionalDataAsync(Control control, SendRequestState sendRequestState)
        {
            SendRequestCompletedEventArgs e;
            try
            {
                var res = sendRequestState.SendAnonymousReportResult;
                if (res.Error != null || res.Cancelled)
                {
                    e = new SendRequestCompletedEventArgs(null, res.Error, res.Cancelled);
                }
                else
                {
                    Response response = res.Result;
                    if (response is ErrorResponse errorResponse)
                        throw new Exception(errorResponse.Error);

                    if (response is NeedReportResponse)
                    {
                        _uploader.SendAdditionalDataCompleted += OnSendAdditionalDataCompleted;
                        _uploader.SendAdditionalDataAsync(response.Context,
                            sendRequestState.GetDetailedExceptionDescription(), sendRequestState);
                        return;
                    }

                    e = new SendRequestCompletedEventArgs(response, null, false);
                }
            }
            catch (Exception ex)
            {
                e = new SendRequestCompletedEventArgs(null, ex, false);
            }

            if (SendRequestCompleted != null)
            {
                if (control != null)
                {
                    control.BeginInvoke(SendRequestCompleted, this, e);
                }
                else
                {
                    SendRequestCompleted.Invoke(this, e);
                }
            }
        }

        private void OnSendAnonymousReportCompleted(object sender, SendAnonymousReportCompletedEventArgs e)
        {
            var state = (SendRequestState)e.UserState;

            bool needToSend;

            lock (state)
            {
                state.SendAnonymousReportResult = e;

                needToSend = state.PrivateData != null;
            }

            if (needToSend)
                SendAdditionalDataAsync(null, state);
        }

        private void OnSendAdditionalDataCompleted(object sender, SendAdditionalDataCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null || e.Cancelled)
                {
                    SendRequestCompleted?.Invoke(this, new SendRequestCompletedEventArgs(null, e.Error, e.Cancelled));
                }

                Response response = e.Result;
                if (response is ErrorResponse errorResponse)
                    throw new Exception(errorResponse.Error);

                SendRequestCompleted?.Invoke(this, new SendRequestCompletedEventArgs(response, null, false));
            }
            catch (Exception ex)
            {
                SendRequestCompleted?.Invoke(this, new SendRequestCompletedEventArgs(null, ex, false));
            }
        }

        public class SendRequestCompletedEventArgs : AsyncCompletedEventArgs
        {
            private readonly Response _result;

            internal SendRequestCompletedEventArgs(Response result, Exception exception, bool cancelled) :
                base(exception, cancelled, null)
            {
                _result = result;
            }

            public Response Result
            {
                get
                {
                    RaiseExceptionIfNecessary();
                    return _result;
                }
            }
        }
    }
}