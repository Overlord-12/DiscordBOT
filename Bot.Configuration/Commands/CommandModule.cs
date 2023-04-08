using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Bot.Configuration.Commands
{
	/// <summary>
	/// Command for discord bot
	/// </summary>
	/// <seealso cref="BaseCommandModule" />
	public class CommandModule : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Заскамила, абобуса");
        }
    }
}
