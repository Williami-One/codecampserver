using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator.Attributes;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.Core.Services.Impl;
using MvcContrib;
using CodeCampServer.UI;
using CodeCampServer.Core.Services;


namespace CodeCampServer.UI.Controllers
{
    public class SponsorController : SaveController<UserGroup, SponsorForm>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IUserGroupSponsorMapper _mapper;
        private readonly ISecurityContext _securityContext;

        public SponsorController(IUserGroupRepository repository, IUserGroupSponsorMapper mapper, ISecurityContext securityContext)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _securityContext = securityContext;
        }


        public ActionResult Index(UserGroup usergroup)
        {
            var entities = usergroup.GetSponsors();

            var entityListDto = (SponsorForm[]) AutoMapper.Mapper.Map(entities, typeof(Sponsor[]), typeof(SponsorForm[]));
            
            return View(entityListDto);
        }

        public ActionResult New(UserGroup userGroup)
        {
            return View("edit",new SponsorForm());  
        }

        [ValidateModel(typeof(SponsorForm))]
        public ActionResult Save(UserGroup userGroup, SponsorForm sponsorForm)
        {
            sponsorForm.ParentID = userGroup.Id;
            return ProcessSave(sponsorForm, entity => RedirectToAction<SponsorController>(s => s.Index(null)));
        }

        public ActionResult Delete(UserGroup userGroup, Sponsor sponsor)
        {
            userGroup.Remove(sponsor);
            _repository.Save(userGroup);
            return RedirectToAction<SponsorController>(c => c.Index(null));
        }


        public ActionResult Edit(UserGroup userGroup, Guid sponsorID)
        {
            var sponsor = userGroup.GetSponsors().Where(sponsor1 => sponsor1.Id == sponsorID).FirstOrDefault();

            return View(AutoMapper.Mapper.Map<Sponsor, SponsorForm>(sponsor));
        }

        public ActionResult List(UserGroup userGroup)
        {
            var entities = userGroup.GetSponsors();

            var entityListDto = (SponsorForm[]) AutoMapper.Mapper.Map(entities, typeof(Sponsor[]), typeof(SponsorForm[]));

            return View("HomePageWidget", entityListDto);
        }
    }
}


