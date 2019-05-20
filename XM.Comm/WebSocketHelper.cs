using SuperSocket.SocketBase;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Comm
{
    public class WebSocketHelper
    {
        public void SetUp()
        {

            WebSocketServer webSocket = new WebSocketServer();
            webSocket.NewSessionConnected += HandlerNewSessionConnected; 
            webSocket.NewMessageReceived += HandlerNewMessageReceived;
            webSocket.SessionClosed += HanderSessionClosed;
            webSocket.Setup("127.0.0.1",4999);
            webSocket.Start();
        }

        /// <summary>
        /// socket连接关闭时触发的事件
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="value"></param>
        private void HanderSessionClosed(WebSocketSession session, CloseReason value)
        {

        }

        /// <summary>
        /// 有新信息传过来的时候触发
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private void HandlerNewMessageReceived(WebSocketSession session, string value)
        {

        }

        /// <summary>
        /// 处理新链接
        /// </summary>
        /// <param name="session"></param>
        private void HandlerNewSessionConnected(WebSocketSession session)
        {

        }
    }
}
