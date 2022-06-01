using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Pages.Dialogs;

namespace Workoutisten.FitStreak.Services
{
    public class ErrorDialogService
    {

        //[Inject]
        public IDialogService DialogService { get; set; }

        public ErrorDialogService(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        public async Task ShowErrorDialog(string statusCode, string errorMessage)
        {
            var parameters = new DialogParameters();
            parameters.Add("ErrorMessage", errorMessage);

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                FullWidth = true,
            };
            var DialogReference = DialogService.Show<ServerErrorDialog>($"Server error {statusCode}", parameters, options);

            await DialogReference.Result;
        }

        public async Task ShowErrorDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ErrorMessage", "We had some trouble reaching the server. Please try again later");

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                FullWidth = true,
            };
            var DialogReference = DialogService.Show<ServerErrorDialog>($"Server error", parameters, options);

            await DialogReference.Result;
        }
    }
}
