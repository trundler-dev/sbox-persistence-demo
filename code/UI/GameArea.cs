using Sandbox.UI;
using Sandbox.UI.Construct;

namespace PersistenceDemo.UI
{
	public partial class GameArea : Panel
	{

		public Label ClickCountLabel { get; set; }
		public Button ClickButton;

		public GameArea()
		{
			StyleSheet.Load( "/UI/Stylesheets/GameArea.scss" );
			ClickButton = Add.Button( "Click Me!" );
			ClickCountLabel = Add.Label( "Clicks: ", "ClickLabel");

			// Add the OnClick Listener to our ClickButton
			ClickButton.AddEventListener( "onclick", () => {
				// Sends an empty message to the WebSocket Server, incrementing the click count.
				PersistenceDemoGame.webSocketExample.Send( "{}" );
			} );
		}

		public override void Tick()
		{
			base.Tick();

			// On Tick, the click count on the UI is updated based on the last message recevied from the WS Server.
			ClickCountLabel.SetText( "Clicks: " + PersistenceDemoGame.webSocketExample.LastMessage );
		}

	}
}
