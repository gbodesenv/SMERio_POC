using Microsoft.AspNet.SignalR;

namespace Rio.SME.Web.Hubs
{
    public class NotificationHub : Hub
    {
        public void Send(string name, string message, int id)
        {
            Clients.Group(name).addNewMessageToPage(message, id);
        }

        public void UsuarioStart(string matricula)
        {
            Groups.Add(Context.ConnectionId, matricula);
        }
    }

    public class UsuarioHub
    {
        public string IdConnection { get; set; }
        public string Matricula { get; set; }
    }
}