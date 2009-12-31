using CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup;
using CodeCampServer.UI.Models.Messages;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class DeleteUserGroupMessageConfiguration : MessageDefinition<DeleteUserGroupInput>
	{
		public DeleteUserGroupMessageConfiguration()
		{
			Execute<DeleteUserGroupCommandMessage>();
		}
	}
}