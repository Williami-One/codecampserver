using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.Impl;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.UI.DataLoader
{
	[TestFixture]
	public class ZDataLoader : DataTestBase
	{
		[Test, Category("DataLoader")]
		public void DataLoader()
		{
//			Logger.EnsureInitialized();
			DependencyRegistrar.EnsureDependenciesRegistered();
			DeleteAllObjects();
			LoadData();
		}

		private void LoadData()
		{
			var mapper = new UserMapper(new UserRepository(GetSessionBuilder()), new Cryptographer());
			User user = mapper.Map(new UserForm
			                       	{
			                       		Name = "Joe User",
			                       		Username = "admin",
			                       		EmailAddress = "joe@user.com",
			                       		Password = "password"
			                       	});


			var userGroup = new UserGroup
			                	{
			                		Name = "Austin .Net Users Group",
			                		City = "Austin",
			                		Region = "Texas",
			                		Country = "USA",
			                		Key = "localhost",
			                		HomepageHTML = "Austin .Net Users Group",
			                	};
			userGroup.Add(user);
			userGroup.Add(new Sponsor
			              	{
			              		Level = SponsorLevel.Platinum,
			              		Name = "Microsoft",
			              		Url = "http://microsoft.com/",
			              		BannerUrl = "http://www.microsoft.com/presspass/images/gallery/logos/web/net_v_web.jpg"
			              	});
			var conference = new Conference
			                 	{
			                 		Address = "123 Guadalupe Street",
			                 		City = "Austin",
			                 		Description = "Texas' Premier Software Community Event",
			                 		EndDate = new DateTime(2009, 5, 10),
			                 		StartDate = new DateTime(2009, 5, 9),
			                 		Key = "austincodecamp",
			                 		LocationName = "St. Edward's Professional Education Center",
			                 		Name = "Austin Code Camp",
			                 		PhoneNumber = "(512) 555-1212",
			                 		PostalCode = "78787",
			                 		Region = "Texas",
			                 		UserGroup = userGroup,
			                 		HtmlContent =
			                 			@"
                                    <p>
<script type=""text/javascript"" src=""http://feeds2.feedburner.com/AustinCodeCamp?format=sigpro""></script>
</p>
<noscript></noscript>
<p><iframe marginwidth=""0"" marginheight=""0"" 
src=""http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=9420+Research+Blvd,+Austin,+TX+78759+(St+Edward's+PEC)&amp;sll=30.384022,-97.743998&amp;sspn=0.006858,0.013626&amp;ie=UTF8&amp;ll=30.397013,-97.74004&amp;spn=0.025911,0.036478&amp;z=14&amp;iwloc=addr&amp;output=embed"" 
frameborder=""0"" width=""425"" scrolling=""no"" height=""350""></iframe><br /><small>
<a style=""color:#0000FF;text-align:left"" 
href=""http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=9420+Research+Blvd,+Austin,+TX+78759+(St+Edward's+PEC)&amp;sll=30.384022,-97.743998&amp;sspn=0.006858,0.013626&amp;ie=UTF8&amp;ll=30.397013,-97.74004&amp;spn=0.025911,0.036478&amp;z=14&amp;iwloc=addr"">
View Larger Map</a></small></p>"
			                 	};


			var list = new List<PersistentObject>
			           	{
			           		user,
			           		userGroup.GetSponsors()[0],
			           		userGroup,
			           		conference,
			           	};

			IEnumerable<Conference> conferences = CreateConferences(userGroup);
			IEnumerable<Meeting> meetings = CreateMeetings(userGroup);
			list.AddRange(conferences.ToArray());
			list.AddRange(meetings.ToArray());

			User[] users = CreateUsers();
			list.AddRange(users);


			PersistEntities(list.ToArray());
		}

		private IEnumerable<Conference> CreateConferences(UserGroup userGroup)
		{
			DateTime startDate = DateTime.Now.AddDays(-7*5).AddMinutes(1);
			for (int i = 0; i < 10; i++)
			{
				DateTime conferenceDate = startDate.AddDays(7*i);
				yield return new Conference
				             	{
				             		Address = "123 Guadalupe Street",
				             		City = "Austin",
				             		Description = "Community Event",
				             		EndDate = conferenceDate.AddDays(1),
				             		StartDate = conferenceDate,
				             		Key = "conference" + i,
				             		LocationName = "St. Edward's Professional Education Center",
				             		Name = "Conference " + i,
				             		PhoneNumber = "(512) 555-1212",
				             		PostalCode = "78787",
				             		Region = "Texas",
				             		UserGroup = userGroup
				             	};
			}
		}

		private IEnumerable<Meeting> CreateMeetings(UserGroup userGroup)
		{
			DateTime startDate = DateTime.Now.AddDays(-7*5);
			for (int i = 0; i < 10; i++)
			{
				DateTime conferenceDate = startDate.AddDays(7*i);
				yield return new Meeting
				             	{
				             		Address = "123 Guadalupe Street",
				             		City = "Austin",
				             		Description = "Community Event",
				             		EndDate = conferenceDate.AddDays(1),
				             		StartDate = conferenceDate,
				             		Key = "meeting" + i,
				             		LocationName = "St. Edward's Professional Education Center",
				             		Name = "Meeting " + i,
				             		PostalCode = "78787",
				             		Region = "Texas",
				             		UserGroup = userGroup,
				             		Topic = "Topic " + i,
				             		Summary = "Summary stuff",
				             		LocationUrl = "http://maps.google.com",
				             		TimeZone = "CST",
				             		SpeakerName = "Jeffrey Palermo",
				             		SpeakerBio = "some bio stuff",
				             		SpeakerUrl = "http://jeffreypalermo.com"
				             	};
			}
		}

		private User[] CreateUsers()
		{
			var mapper = new UserMapper(new UserRepository(GetSessionBuilder()), new Cryptographer());
			User user = mapper.Map(new UserForm
			                       	{
			                       		Name = "Joe User",
			                       		Username = "admin",
			                       		EmailAddress = "joe@user.com",
			                       		Password = "password"
			                       	});
			return new[]
			       	{
			       		mapper.Map(new UserForm
			       		           	{
			       		           		Name = "Jeffrey Palermo",
			       		           		EmailAddress = "jeffis@theman.com",
			       		           		Username = "jpalermo",
			       		           		Password = "beer"
			       		           	}),
			       		mapper.Map(new UserForm
			       		           	{
			       		           		Name = "Homer Simpson",
			       		           		EmailAddress = "homer@simpsons.com",
			       		           		Username = "hsimpson",
			       		           		Password = "beer"
			       		           	}),
			       		mapper.Map(new UserForm
			       		           	{
			       		           		Name = "Bart Simpson",
			       		           		EmailAddress = "bart@simpsons.com",
			       		           		Username = "bsimpson",
			       		           		Password = "beer"
			       		           	})
			       	};
		}

		private static int _seed;

		private static bool RandomlyDecideWhetherToSkip()
		{
			int index = new Random(_seed += GetRandomInt()).Next(0, 2);
			if (index == 0) return true;
			return false;
		}

		private static int GetRandomInt()
		{
			return new Random(_seed++).Next(100);
		}
	}
}