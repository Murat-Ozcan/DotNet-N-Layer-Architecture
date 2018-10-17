using System.Collections.Generic;

namespace MRTFramework.Model.InApps.LogModel
{
    public class LogDetail
    {
        public string NameSpace { get; set; }
        public string MethodName { get; set; }
        public string CodeLine { get; set; }
        public string CustomMessage { get; set; }
        public string AspectName { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}
