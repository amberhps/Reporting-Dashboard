using Microsoft.AspNetCore.Components;

namespace ReportingDashboard.Components.Pages.Common;

public class CancellableComponentBase : ComponentBase, IDisposable
{
    private CancellationTokenSource? cancellationTokenSource;
    protected CancellationToken NavigationToken => (cancellationTokenSource ??= new()).Token;

    public virtual void Dispose()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;

            GC.SuppressFinalize(this);
        }
    }
}
