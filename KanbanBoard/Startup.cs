using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KanbanBoard.Startup))]
namespace KanbanBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
