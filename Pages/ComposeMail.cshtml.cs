// ComposeEmail.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ComposeEmailModel : PageModel
{
    [BindProperty]
    public string To { get; set; }

    [BindProperty]
    public string Cc { get; set; }

    [BindProperty]
    public string Subject { get; set; }

    [BindProperty]
    public string Message { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Add your email sending logic here
        // For example:
        // await _emailService.SendEmailAsync(To, Cc, Subject, Message);

        // Redirect back to the inbox or show success message
        return RedirectToPage("./Index");
    }
}