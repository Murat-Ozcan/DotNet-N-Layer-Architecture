using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.Model.BaseModels.Concrete
{
    public class Log : IBaseEntity
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string Thread { get; set; }
        public string Exception { get; set; }
    }
}
