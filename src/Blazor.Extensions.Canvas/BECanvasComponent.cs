using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blazor.Extensions
{
    public class BECanvasComponent : ComponentBase, IAsyncDisposable
    {
        [Parameter]
        public long Height { get; set; }

        [Parameter]
        public long Width { get; set; }

        protected readonly string Id = Guid.NewGuid().ToString();
        protected ElementReference _canvasRef;

        public ElementReference CanvasReference => this._canvasRef;

        [Inject]
        internal IJSRuntime JSRuntime { get; set; }

        private IJSObjectReference _module;
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this._module = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Blazor.Extensions.Canvas/blazor.extensions.canvas.js");
            }
        }
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (this._module is not null)
            {
                await this._module.DisposeAsync();
            }
        }

    }
}
