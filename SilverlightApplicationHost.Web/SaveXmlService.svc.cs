#region

using System.ServiceModel;
using System.ServiceModel.Activation;

#endregion

namespace SilverlightApplicationHost.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SaveXmlService
    {
        [OperationContract]
        public void SaveXml(string xmlData)
        {
            // Добавьте здесь реализацию операции
        }

        // Добавьте здесь дополнительные операции и отметьте их атрибутом [OperationContract]
    }
}