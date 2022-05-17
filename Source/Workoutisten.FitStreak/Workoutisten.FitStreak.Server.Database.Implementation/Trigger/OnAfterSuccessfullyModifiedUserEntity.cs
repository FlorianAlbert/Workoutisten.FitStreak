using EntityFrameworkCore.Triggered;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Trigger
{
    public class OnAfterSuccessfullyModifiedUserEntity : IAfterSaveTrigger<User>
    {
        private IRepository Repository { get; }

        public OnAfterSuccessfullyModifiedUserEntity(IRepository repository)
        {
            Repository = repository;
        }

        public async Task AfterSave(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Added:
                    await OnUserAdded(context, cancellationToken);
                    break;
                case ChangeType.Modified:
                    await OnUserModified(context, cancellationToken);
                    break;
                case ChangeType.Deleted:
                    await OnUserDeleted(context, cancellationToken);
                    break;
            }
        }

        private async Task OnUserDeleted(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            // ToDo: Verabschiedungsmail direkt über Mailservice versenden,
            // da user nicht mehr besteht und deshalb in der DB keine Mail mehr angehängt werden kann
        }

        private async Task OnUserModified(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            if (!context.UnmodifiedEntity.IsVerified && context.Entity.IsVerified)
            {
                var verificationMail = new Email
                {
                    Subject = "Bestätigung der Account Verifikation!",
                    Message = $"Hallo {context.Entity.FirstName}!\n" +
                    $"\n" +
                    $"du hast vor kurzem deinen Account erfolgreich bestätigt! " +
                    $"So kannst du jetzt loslegen und sofort mit dem Pumpen beginnen.\n" +
                    $"Damit dir der Einstieg etwas leichter fällt haben wir dir direkt zu Beginn einige Übungen angelegt. " +
                    $"Du kannst diese aber natürlich auch wieder löschen und mit eigenen Übungen arbeiten. Deiner Kreativität sind " +
                    $"in dieser Hinsicht keine Grenzen gesetzt!\n" +
                    $"\n" +
                    $"Jetzt zeig uns mal was du drauf hast! Viel Erfolg!\n" +
                    $"\n" +
                    $"Dein FitStreak-Team"
                };

                verificationMail.Receivers.Add(context.Entity);

                await Repository.CreateOrUpdateAsync(verificationMail);
            }

            if (!string.IsNullOrEmpty(context.Entity.PasswordForgottenKey) &&
                context.Entity.PasswordForgottenKey != context.UnmodifiedEntity.PasswordForgottenKey)
            {
                await GeneratePasswordForgottenEmail(context.Entity);
            }
        }

        private async Task GeneratePasswordForgottenEmail(User user)
        {
            var resetMail = new Email
            {
                Subject = "Beantragung eines Passwort-Resets!",
                Message = $"Hallo {user.FirstName}!\n" +
                    $"\n" +
                    $"du hast einen Reset deines Passwortes beantragt. " +
                    $"Um dein Passwort zurück zu setzen musst du in der App deine Email und den folgenden Code eingeben:" +
                    $"\n" +
                    $"{user.PasswordForgottenKey}\n" +
                    $"\n" +
                    $"Sobald das erledigt ist, kannst du dich normal mit deinem neuen Passwort anmelden!\n" +
                    $"Und immer dran denken: Bleib geschmeidig!\n" +
                    $"\n" +
                    $"Dein FitStreak-Team"
            };

            resetMail.Receivers.Add(user);
            await Repository.CreateOrUpdateAsync(resetMail);
        }

        private async Task OnUserAdded(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            if (!context.Entity.IsVerified)
            {
                var verificationMail = new Email
                {
                    Subject = "Willkommen bei FitStreak!",
                    Message = $"Hallo { context.Entity.FirstName }!\n" +
                    $"\n" +
                    $"wir freuen uns, dass du dich für FitStreak interessierst und in unserer Community mitwirken möchtest! " +
                    $"Damit du so richtig loslegen kannst, musst du vorher allerdings noch deinen Account bestätigen. " +
                    $"Bitte gib dafür den folgenden Code in deiner App ein:\n" +
                    $"\n" +
                    $"{ context.Entity.RegistrationConfirmationKey }\n" +
                    $"\n" +
                    $"Sobald das erledigt ist, kannst du sofort loslegen!\n" +
                    $"Und immer dran denken: Bleib geschmeidig!\n" +
                    $"\n" +
                    $"Dein FitStreak-Team"
                };

                verificationMail.Receivers.Add(context.Entity);

                await Repository.CreateOrUpdateAsync(verificationMail);
            }

            // ToDo: Starter-Exercises und evtl Workouts hinzufügen
        }
    }
}
