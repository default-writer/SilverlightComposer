﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SilverlightApplicationHost</title>
        <style type="text/css">
            html, body {
                height: 100%;
                overflow: auto;
            }

            body {
                margin: 0;
                padding: 0;
            }

            #silverlightControlHost {
                height: 100%;
                text-align: center;
            }
        </style>
        <script type="text/javascript" src="Silverlight.js"></script>
        <script type="text/javascript">
            var slCtl = null;

            function DisplayAlertMessage(param1) {
                alert(param1);
            }

            function pluginLoaded(sender, args) {
                slCtl = sender.getHost();
            }

            function CallSilverlight(msg) {
                slCtl.Content.SL2JS.ShowAlertPopup(msg);
            }
        </script>
        <script type="text/javascript">
            function onSilverlightError(sender, args) {
                var appSource = "";
                if (sender != null && sender != 0) {
                    appSource = sender.getHost().Source;
                }

                var errorType = args.ErrorType;
                var iErrorCode = args.ErrorCode;

                if (errorType == "ImageError" || errorType == "MediaError") {
                    return;
                }

                var errMsg = "Необработанная ошибка в приложении Silverlight " + appSource + "\n";

                errMsg += "Код: " + iErrorCode + "    \n";
                errMsg += "Категория: " + errorType + "       \n";
                errMsg += "Сообщение: " + args.ErrorMessage + "     \n";

                if (errorType == "ParserError") {
                    errMsg += "Файл: " + args.xamlFile + "     \n";
                    errMsg += "Строка: " + args.lineNumber + "     \n";
                    errMsg += "Позиция: " + args.charPosition + "     \n";
                } else if (errorType == "RuntimeError") {
                    if (args.lineNumber != 0) {
                        errMsg += "Строка: " + args.lineNumber + "     \n";
                        errMsg += "Позиция: " + args.charPosition + "     \n";
                    }
                    errMsg += "Имя метода: " + args.methodName + "     \n";
                }
                throw new Error(errMsg);
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server" style="height: 100%">
            <div id="silverlightControlMessage" style="height: auto; visibility: visible; width: 100%;">
            </div>
            <div id="silverlightControlHost">
                <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
                    <param name="source" value="ClientBin/SilverlightApplicationHost.xap" />
                    <param name="onError" value="onSilverlightError" />
                    <param name="background" value="white" />
                    <param name="minRuntimeVersion" value="5.0.61118.0" />
                    <param name="autoUpgrade" value="true" />
                    <param name="onLoad" value="pluginLoaded" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0" style="text-decoration: none">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Получить Microsoft Silverlight" style="border-style: none" />
                    </a>
                </object>
                <iframe id="_sl_historyFrame" style="border: 0px; height: 0px; visibility: hidden; width: 0px;"></iframe>
            </div>
        </form>
    </body>
</html>