using Sandbox;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersistenceDemo.Helpers
{
	/// <summary>
	/// WebSocketExample shows example s&box WebSocket implementation
	/// </summary>
	public class WebSocketExample
	{
		private WebSocket _webSocket;
		private readonly string _connectString = "ws://137.184.206.17:8888/";
		private readonly int _maxMessageSize = 128;

		/// <summary>
		/// LastMessage stores the last message received.
		/// </summary>
		public string LastMessage { get; set; }

		/// <summary>
		/// Constructor for WebSocketExample
		/// </summary>
		public WebSocketExample()
		{
			_webSocket = new WebSocket( _maxMessageSize );
			_webSocket.OnMessageReceived += MessageReceived;

		}

		/// <summary>
		/// Connect to the remote WebSocket Server.
		/// </summary>
		/// <param name="path">The path appended to the connection string.</param>
		/// <returns>Whether the WebSocket is connected.</returns>
		public async Task<bool> Connect(string path)
		{
			await _webSocket.Connect( _connectString + path );
			return _webSocket.IsConnected;
		}

		/// <summary>
		/// Method for disposing of the WebSocket.
		/// </summary>
		public void Shutdown()
		{
			_webSocket?.Dispose();
			_webSocket = null;
		}

		/// <summary>
		/// Asynchronous method for Sending a message to the WebSocket Server.
		/// </summary>
		/// <param name="message">The message to send to the WebSocket Server.</param>
		public async void Send( string message )
		{
			string jsonString = JsonSerializer.Serialize( message );
			await _webSocket.Send( jsonString );
			
		}

		/// <summary>
		/// Method to handle a received message.
		/// </summary>
		/// <param name="message">The message being received.</param>
		private void MessageReceived( string message )
		{
			LastMessage = message;
		}


	}
}
