using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebAPI.Core.Model;
using WebAPI.Service.Contacts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactController : ControllerBase
    {
        private readonly IContactServices _contactServices;

        public ContactController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        [HttpGet("ContactList")]
        public ActionResult<IEnumerable<ContactModel>> GetContactList()
        {
            var result = _contactServices.GetContactList();


            return Ok(result);
        }

        [HttpGet("GetContactPaginationList")]
        public ActionResult<ContactPagingList> GetContactPageList(int currentPage = 1, int pageSize = 10)
        {
            var result = _contactServices.GetContactPagingList(currentPage, pageSize);

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }


        [HttpGet("GetById")]
        public ActionResult<ContactModel> GetContactDetailById(int Id)
        {
            var result = _contactServices.GetContactById(Id);

            if(result.Id != 0)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost("AddContact")]
        public ActionResult<bool> Create(ContactModel model)
        {          
            var response = _contactServices.AddContactDetails(model);

            if (response)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }

        }

        [HttpPost("EditContact")]
        public ActionResult<bool> EditContactDetail(ContactModel model)
        {
            var response = _contactServices.EditContact(model);

            if (response)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }

        }

        [HttpDelete("DeleteContact")]
        public ActionResult<bool> DeletedContactDetail(int Id)
        {
            var response = _contactServices.DeleteContact(Id);

            if (response)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }

        }

    }
}