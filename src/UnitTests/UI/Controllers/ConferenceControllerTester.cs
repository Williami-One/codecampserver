﻿using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
    public class When_a_conference_does_not_exist : TestControllerBase<ConferenceController>
    {
        private IConferenceRepository conferenceRepository;

        protected override ConferenceController CreateController()
        {
            conferenceRepository = Mock<IConferenceRepository>();
            conferenceRepository.Stub(repo => repo.GetAll()).Return(new Conference[0]);

            return new ConferenceController(conferenceRepository);
        }

        [Test]
        public void Edit_should_render_the_edit_view()
        {
            ActionResult result = controllerUnderTest.Edit();
            result
                .AssertViewRendered()
                .ForView(DEFAULT_VIEW)
                .WithViewData<ConferenceForm>()
                .ShouldNotBeNull();

            conferenceRepository.AssertWasCalled(r => r.Save(null), o => o.IgnoreArguments());
        }

        [Test]
        public void Index_action_should_redirect_to_edit_when_conference_does_not_exist()
        {
            ActionResult result = controllerUnderTest.Index();

            result.AssertActionRedirect().ToAction<ConferenceController>(a => a.Edit());
        }
    }

    public class When_a_conference_exists : TestControllerBase<ConferenceController>
    {
        private IConferenceRepository _conferenceRepository;
        private Conference conference;

        protected override ConferenceController CreateController()
        {
            conference = new Conference {Name = "Austin Code Camp", Id = Guid.NewGuid()};
            _conferenceRepository = Mock<IConferenceRepository>();
            _conferenceRepository.Stub(repo => repo.GetAll()).Return(new[] {conference});

            return new ConferenceController(_conferenceRepository);
        }

        [Test]
        public void Save_should_a_valid_user()
        {
            var form = new ConferenceForm {Id = conference.Id, Name = "Austin Code Camp"};

            _conferenceRepository.Stub(c => c.GetById(conference.Id)).Return(conference);

            ActionResult result = controllerUnderTest.Save(form);

            result
                .AssertActionRedirect()
                .ToAction<ConferenceController>(a => a.Index());

            conference.Name.ShouldEqual("Austin Code Camp");

            _conferenceRepository.AssertWasCalled(r => r.Save(conference));
        }
    }
}