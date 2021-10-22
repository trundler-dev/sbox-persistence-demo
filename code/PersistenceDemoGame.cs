using Sandbox;
using PersistenceDemo.UI;
using PersistenceDemo.Helpers;

namespace PersistenceDemo
{
	public partial class PersistenceDemoGame : Game
	{

		public static WebSocketExample WebSocketExample { get; set; }

		public PersistenceDemoGame()
		{
			if ( IsServer )
				new MyGameHud();
		}

		public override void ClientJoined( Client cl )
		{
			base.ClientJoined( cl );

			StartWebSocketRpc( To.Single( cl ) );
		}

		/// <summary>
		/// Asynchronous Rpc to start the initialize the WebSocketExample Wrapper and Connect to the WS Server.
		/// </summary>
		[ClientRpc]
		private async void StartWebSocketRpc()
		{
			WebSocketExample = new WebSocketExample();
			bool isConnected = await WebSocketExample.Connect( "clicks" );
			if ( isConnected ) Log.Info( "Connection to WS Server Successful" );
		}

	}
}

