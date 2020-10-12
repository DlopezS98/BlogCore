using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBlogCore.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly SignInManager<ApplicationUserModel> _signInManager;

        public IndexModel(
            UserManager<ApplicationUserModel> userManager,
            SignInManager<ApplicationUserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Teléfono")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            [Display(Name = "Nombre")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "La dirección es obligatoria")]
            [Display(Name = "Dirección")]
            public string Address { get; set; }

            [Required(ErrorMessage = "La ciudad es obligatoria")]
            [Display(Name = "Ciudad")]
            public string City { get; set; }

            [Required(ErrorMessage = "El país es obligatoria")]
            [Display(Name = "País")]
            public string Country { get; set; }
        }

        private async Task LoadAsync(ApplicationUserModel user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                Address = user.Address,
                City = user.City,
                Country = user.Country
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.FirstName = Input.FirstName;
            user.Address = Input.Address;
            user.City = Input.City;
            user.Country = Input.Country;
            user.PhoneNumber = user.PhoneNumber;
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Su perfil se actualizado correctamente";
            return RedirectToPage();
        }
    }
}
