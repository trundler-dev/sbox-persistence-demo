using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using PersistenceDemo.Helpers;

namespace PersistenceDemo.UI
{
	public partial class ChatEntry : Panel
	{
		public Image Avatar;
		public Label NameLabel;
		public Label MessageLabel;

		public ChatEntry( string name, string avatar, string message )
		{
			Avatar = Add.Image( avatar );
			NameLabel = Add.Label( name, "NameLabel" );
			MessageLabel = Add.Label( message, "MessageLabel" );
		}
	}

	public partial class ChatBox : Panel
	{

		public static ChatBox Instance;
		public Panel ChatWrapper;
		public TextEntry ChatInput;

		public ChatBox()
		{
			StyleSheet.Load( "/UI/Stylesheets/ChatBox.scss" );

			Instance = this;

			Add.Label( "Chat", "ChatLabel" );
			ChatWrapper = Add.Panel( "ChatWrapper" );
			ChatWrapper.PreferScrollToBottom = true;

			ChatInput = Add.TextEntry( "" );
			ChatInput.Placeholder = Consts.CHAT_INPUT_HELP_MESSAGE;
			ChatInput.AddEventListener( "onsubmit", () => Submit() );
			ChatInput.AddEventListener( "onblur", () => CloseChat() );

			Chat.OnOpenChat += OpenChat;

		}

		private void OpenChat()
		{
			ChatInput.Placeholder = "";
			ChatInput.Focus();
		}

		private void CloseChat()
		{
			ChatInput.Placeholder = Consts.CHAT_INPUT_HELP_MESSAGE;
			ChatInput.Blur();
		}

		private void Submit()
		{
			CloseChat();

			string message = ChatInput.Text.Trim();
			ChatInput.Text = "";

			if ( string.IsNullOrWhiteSpace( message ) ) return;

			Say( message );
		}

		public void AddEntry( string name, string avatar, string message )
		{
			ChatWrapper.AddChild(new ChatEntry(name, avatar, message));
		}

		[ServerCmd( "say" )]
		public static void Say( string message )
		{
			Assert.NotNull( ConsoleSystem.Caller );

			if ( message.Contains( '\n' ) || message.Contains( '\r' ) )
				return;

			Log.Info( $"{ConsoleSystem.Caller}: {message}" );
			CreateChatEntry( To.Everyone, ConsoleSystem.Caller.Name, $"avatar:{ConsoleSystem.Caller.SteamId}", message );
		}

		[ClientCmd("chat_add", CanBeCalledFromServer = true)]
		public static void CreateChatEntry( string name, string avatar, string message )
		{
			Instance?.AddEntry( name, avatar, message );
		}
	}
}
