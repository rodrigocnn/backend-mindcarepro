using MindCarePro.Application.Enums;
using MindCarePro.Application.Enums.MindCarePro.Application.Enums;

namespace MindCarePro.Application.Utils;

public static class AppointmentColors
{
    public static (string Background, string Text) GetColors(string status)
    {
        return status switch
        {
            AppointmentStatus.Schedule   => ("#9ca3af", "#fff"), // cinza
            AppointmentStatus.Confirmed  => ("#0284c7", "#fff"), // azul
            AppointmentStatus.Reschedule => ("#e6be1a", "#000"), // amarelo
            AppointmentStatus.Completed  => ("#10b981", "#fff"), // verde
            AppointmentStatus.Canceled   => ("#ef4444", "#fff"), // vermelho
            _                            => ("#9ca3af", "#fff"), // fallback
        };
    }
}
