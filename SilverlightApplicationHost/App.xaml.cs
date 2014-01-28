#region

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;
using SilverlightApplicationHost.Interaction;
using SilverlightApplicationHost.Services;
using SilverlightApplicationHost.Views;

#endregion

namespace SilverlightApplicationHost
{
    public partial class App
    {
        public App()
        {
            Startup += Application_Startup;
            Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var myScript = new ScriptableClass();
            HtmlPage.RegisterScriptableObject("SL2JS", myScript);
            try
            {
                CatalogService.Initialize();
                RootVisual = new MainPage();
            }
            catch (Exception ex)
            {
                HtmlPage.Window.Invoke("DisplayAlertMessage", string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // Если приложение выполняется вне отладчика, воспользуйтесь для сообщения об исключении
            // механизмом исключений браузера. В IE исключение будет отображаться в виде желтого значка оповещения 
            // в строке состояния, а в Firefox - в виде ошибки скрипта.
            if (!Debugger.IsAttached)
            {
                // ПРИМЕЧАНИЕ. Это позволит приложению выполняться после того, как исключение было выдано,
                // но не было обработано. 
                // Для рабочих приложений такую обработку ошибок следует заменить на код, 
                // оповещающий веб-сайт об ошибке и останавливающий работу приложения.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportError(e));
            }
        }

        private void ReportError(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }
    }
}