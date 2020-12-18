using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IWidgetRepository : IRepository<Widget, int>
    {
        List<Widget> GetFullWidgets();
    }
}
