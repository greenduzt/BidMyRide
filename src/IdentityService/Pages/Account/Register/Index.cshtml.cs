using System.Security.Claims;
using Duende.IdentityModel;
using IdentityService.Models;
using IdentityService.Pages.Account.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Register
{
   [SecurityHeaders]
   [AllowAnonymous]
    public class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Index(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; } 
        
        [BindProperty]
        public bool RegisterSuccess { get; set; }

        public IActionResult OnGet(string ReturnUrl)
        {
            Input = new RegisterViewModel{
                ReturnUrl = ReturnUrl
            };

            return Page();// Then return the page to the user
        }

        //Post method to handle the form submission
        public async Task<IActionResult> OnPost()
        {

            //If the user clicked on the cancel button
            if(Input.Button != "register")           
                return Redirect("~/");//Go back to the home page

                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = Input.UserName,
                        Email = Input.Email,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        // Add the user to the default role
                        await _userManager.AddClaimsAsync(user, new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, Input.FullName)
                        });

                       RegisterSuccess = true;
                      
                    }                  
                }
           
                return Page();
          
        }
    }
}
