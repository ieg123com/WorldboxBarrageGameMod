using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

namespace BarrageGame
{
    public class WSocketClientHelp
    {
        ClientWebSocket ws = null;
        Uri uri = null;
        bool isUserClose = false;//是否最后由用户手动关闭

        /// <summary>
        /// WebSocket状态
        /// </summary>
        public WebSocketState? State { get => ws?.State; }

        /// <summary>
        /// 包含一个数据的事件
        /// </summary>
        public delegate void MessageEventHandler(object sender, string data);
        public delegate void ErrorEventHandler(object sender, Exception ex);

        /// <summary>
        /// 连接建立时触发
        /// </summary>
        public event EventHandler OnOpen;
        /// <summary>
        /// 客户端接收服务端数据时触发
        /// </summary>
        public event MessageEventHandler OnMessage;
        /// <summary>
        /// 通信发生错误时触发
        /// </summary>
        public event ErrorEventHandler OnError;
        /// <summary>
        /// 连接关闭时触发
        /// </summary>
        public event EventHandler OnClose;

        
        public WSocketClientHelp(string wsUrl)
        {
            uri = new Uri(wsUrl);
            ws = new ClientWebSocket();
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        public void Open()
        {
            Task.Run(async () =>
            {
                if (ws.State == WebSocketState.Connecting || ws.State == WebSocketState.Open)
                    return;

                string netErr = string.Empty;
                try
                {
                    //初始化链接
                    isUserClose = false;
                    ws = new ClientWebSocket();
                    await ws.ConnectAsync(uri, CancellationToken.None);
                    Main.actions.Enqueue(() =>{
                        Debug.Log("连接成功");
                    });
                    if (OnOpen != null)
                        Main.actions.Enqueue(() =>
                        {
                            OnOpen(ws, new EventArgs());
                        });
                        

                    //全部消息容器
                    List<byte> bs = new List<byte>();
                    //缓冲区
                    var buffer = new byte[1024 * 4];
                    //监听Socket信息
                    WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    //是否关闭
                    while (!result.CloseStatus.HasValue)
                    {
                        //文本消息
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            bs.AddRange(buffer.Take(result.Count));

                            //消息是否已接收完全
                            if (result.EndOfMessage)
                            {
                                //发送过来的消息
                                string userMsg = Encoding.UTF8.GetString(bs.ToArray(), 0, bs.Count);

                                if (OnMessage != null)
                                    Main.actions.Enqueue(() =>
                                    {
                                        OnMessage(ws, userMsg);
                                    });
                               

                                //清空消息容器
                                bs = new List<byte>();
                            }
                        }
                        //继续监听Socket信息
                        result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    }
                    ////关闭WebSocket（服务端发起）
                    //await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    netErr = " .Net发生错误" + ex.Message;

                    if (OnError != null)
                        OnError(ws, ex);

                    //if (ws != null && ws.State == WebSocketState.Open)
                    //    //关闭WebSocket（客户端发起）
                    //    await ws.CloseAsync(WebSocketCloseStatus.Empty, ex.Message, CancellationToken.None);
                }
                finally
                {
                    if (!isUserClose)
                        Close(ws.CloseStatus.Value, ws.CloseStatusDescription + netErr);


                }

                Main.actions.Enqueue(() =>{
                    Debug.Log($"WebSocket Over... {isUserClose}");
                });
                
            });

        }

        /// <summary>
        /// 使用连接发送文本消息
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="mess"></param>
        /// <returns>是否尝试了发送</returns>
        public bool Send(string mess)
        {
            if (ws.State != WebSocketState.Open)
                return false;

            Task.Run(async () =>
            {
                var replyMess = Encoding.UTF8.GetBytes(mess);
                //发送消息
                await ws.SendAsync(new ArraySegment<byte>(replyMess), WebSocketMessageType.Text, true, CancellationToken.None);
            });

            return true;
        }

        /// <summary>
        /// 使用连接发送字节消息
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="mess"></param>
        /// <returns>是否尝试了发送</returns>
        public bool Send(byte[] bytes)
        {
            if (ws.State != WebSocketState.Open)
                return false;

            Task.Run(async () =>
            {
                //发送消息
                await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Binary, true, CancellationToken.None);
            });

            return true;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            isUserClose = true;
            Close(WebSocketCloseStatus.NormalClosure, "用户手动关闭");
        }

        public void Close(WebSocketCloseStatus closeStatus, string statusDescription)
        {
            Task.Run(async () =>
            {
                try
                {
                    //关闭WebSocket（客户端发起）
                    await ws.CloseAsync(closeStatus, statusDescription, CancellationToken.None);
                }
                catch (Exception ex)
                {

                }

                ws.Abort();
                ws.Dispose();

                Main.actions.Enqueue(() =>{
                    Debug.Log($"连接断开 1{statusDescription}");
                });

                
                if (OnClose != null)
                    Main.actions.Enqueue(() =>
                    {
                        OnClose(ws, new EventArgs());
                    });
                
            });
        }
        public void Update()
        {

        }

    }

}