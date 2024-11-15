using Chat.SampleApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Chat.SampleApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://test-chat-36e47-default-rtdb.firebaseio.com") });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IFirebaseService, FirebaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
builder.Services.AddScoped<IChatService, ChatService>();

await builder.Build().RunAsync();
