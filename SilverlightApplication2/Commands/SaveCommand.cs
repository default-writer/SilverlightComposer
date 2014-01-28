#region

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Browser;
using System.Xml;
using SilverlightApplication2.SaveXmlServiceReference;
using SilverlightApplicationHost.Interfaces.Commands;
using SilverlightApplicationHost.Interfaces.Models;

#endregion

namespace SilverlightApplication2.Commands
{
    public class SaveCommand : ISaveCommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            var collection = parameter as ObservableCollection<IDataItem>;
            if (collection != null)
            {
                var data = (ObservableCollection<IDataItem>) parameter;
                using (var stringWriter = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings {Indent = true}))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("data");
                        foreach (var dataItem in data)
                        {
                            writer.WriteStartElement("item");
                            writer.WriteString(dataItem.Value);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                    }
                    stringWriter.Close();
                    var client = new SaveXmlServiceClient();
                    client.SaveXmlCompleted += (sender, args) => HtmlPage.Window.Invoke("DisplayAlertMessage", string.Format("{0}\r\n{1}", args.Error != null ? args.Error.Message : "", args.Cancelled ? "Cancel" : "OK"));
                    client.SaveXmlAsync(stringWriter.ToString());
                }
            }
        }
    }
}