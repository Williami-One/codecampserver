using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class TimeSlotRepositoryTester : RepositoryTester<TimeSlot, ITimeSlotRepository>
	{
		protected override ITimeSlotRepository CreateRepository()
		{
			return new TimeSlotRepository(GetSessionBuilder());
		}
	}
}