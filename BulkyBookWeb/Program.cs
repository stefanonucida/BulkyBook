
//crea un istanza del builder che creerà la nostra app. 
//args è una lista di stringhe opzionali passate al programma dal comando dotnet 
//questi args saranno poi usati per la configurazione dell'applicazione
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// qui registriamo nel motore DI tutte le dipendenze del progetto.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//registrazione nel motore DI di un db
builder.Services.AddDbContext<ApplicationDbContext>(optionsAction: options => 
//dopo aver installato il pacchetto nuget per lavorare con sql avremo il metodo UseSqlServer
options.UseSqlServer(
    //Recuperiamo la stringa di connessione da appsettings.json. Attenzione qui viene fatto l'accesso diretto alla chiave "DefaultConnection"
    //del blocco "ConnectionString", un blocco di default
    builder.Configuration.GetConnectionString("DefaultConnection") ));


//nb tutte le dipendendenze (es db, librerie particolari etc) vanno registrate PRIMA DI LANCIARE IL builder.Build().
var app = builder.Build();

// Inizio definizione pipeline http ovvero il codice eseguito da dotnet in fase di ricezione richiesta da parte del client
//attenzione che la pipeline sarà eseguita ESATTAMENTE nell'ordine che viene definito nel codice in calce.

if (!app.Environment.IsDevelopment())
{
    //se non stai lavorando in sviluppo ritorna la pagina di errore generico
    app.UseExceptionHandler("/Home/Error");
    // usa hsts (HTTP STRICT TRANSPORT SECURITY) ovvero solo connessioni strettamente https
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else 
{
    //altrimenti usa la pagina di gestione eccezioni per developer in modo da vedere bene il messaggio
    app.UseDeveloperExceptionPage();    
}

app.UseHttpsRedirection();

//usa i file statici contenuti in wwwroot
app.UseStaticFiles();

app.UseRouting();

//se dobbiamo usare autenticazione, nella pipeline dobbiamo usare PRIMA authentication e DOPO authorization. in quanto per autorizzare una persona
//ad usare un determinato file, dobbiamo prima AUTENTICARLA ovvero sapere chi è, se può loggarsi etc.
app.UseAuthentication();

app.UseAuthorization();

//ogni richiesta dovrà seguire il seguente pattern: /controller/action/id

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//fine definizione pipeline

app.Run();
