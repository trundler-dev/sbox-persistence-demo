using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;

namespace PersistenceDemo.UI
{
	public partial class PlayerEntry : Panel
	{
		public Label NameLabel;
		public Image Avatar;

		public PlayerEntry( Client client )
		{
			Avatar = Add.Image( $"avatar:{client.SteamId}" );
			NameLabel = Add.Label( $"{client.Name}", "NameLabel" );
		}
	}

	public partial class PlayerList : Panel
	{

		Dictionary<Client, PlayerEntry> activeEntries = new Dictionary<Client, PlayerEntry>();
		Panel scrollablePanel;

		public PlayerList()
		{
			StyleSheet.Load( "/UI/Stylesheets/PlayerList.scss" );
			Add.Label( "Players" );
			scrollablePanel = Add.Panel( "Scrollable" );
		}

		public override void Tick()
		{
			base.Tick();

			var deleteList = new List<Client>();
			deleteList.AddRange( activeEntries.Keys );

			foreach ( var client in Entity.All.OfType<Client>() )
			{
				deleteList.Remove( client );

				if (!activeEntries.TryGetValue(client, out var entry) )
				{
					entry = CreatePlayerEntry( client );
					activeEntries[client] = entry;
				}
			}
		}

		public PlayerEntry CreatePlayerEntry(Client client)
		{
			var entry = new PlayerEntry( client )
			{
				Parent = scrollablePanel
			};
			return entry;
		}
	}
}
