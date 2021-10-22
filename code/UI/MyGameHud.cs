using Sandbox;
using Sandbox.UI;

namespace PersistenceDemo.UI
{
	public partial class MyGameHud : HudEntity<RootPanel>
	{
		public MyGameHud()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/UI/Stylesheets/MyGameHud.scss" );
			RootPanel.AddChild<GameArea>();
			RootPanel.AddChild<SideBar>();
		}
	}
}
