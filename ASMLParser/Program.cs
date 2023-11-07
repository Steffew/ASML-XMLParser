using Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//SQL connection
 /* using (SqlConnection connection = new SqlConnection("Data Source=mssqlstud.fhict.local;Initial Catalog=dbi458166_asmleda;Persist Security Info=True;User ID=dbi458166_asmleda;Password=Mr36733duBG2"))
{
   try
   {
       connection.Open();
   }
   catch
   {
       throw new Exception("Database connection error.");
   }

   string sql = "SELECT @@VERSION";
   using (SqlCommand command = new SqlCommand(sql, connection))
   {
       string version = command.ExecuteScalar().ToString();
       Console.WriteLine($"SQL version is: {version}");
   }

   connection.Close();
} */


// Test LoadData
ServerConnection test = new();
test.LoadData();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
