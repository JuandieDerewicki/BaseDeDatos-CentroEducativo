using CentroEducativoAPISQL.Modelos;
using CentroEducativoAPISQL.Servicios;
using CentroEducativoAPISQL.Controladores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// En services que son todas las clases que estan por venir y los controladores que estamos por usar, le vamos a añadir el contexto que estamos creando

// Esta clase es el punto de entrada de la aplicacion que utiliza ASP.NET Core para construir una API.
var builder = WebApplication.CreateBuilder(args); // Se inicia la construccion de la aplicacion mediante la creacion de este objeto a partir de builder, se utiliza la instancia para configurar y construir la aplicacion 

builder.Services.AddCors(option =>
{
    option.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.SetIsOriginAllowed
        (origin => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

// Se obtiene la cadena de conexion a la BD desde la config de la aplicacion, mientras la cadena se almccena en el appsetings .json y se recupera aca para configurar la BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Estamos compartiendo la cadena de conexion a toda la API

// Luego se configura EF para utilizar la cadena de conexion especificada para establecer una conexion a la BD SQL Server
builder.Services.AddDbContext<MiDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Aqui se hace la configuracion de los servicios que realizan las tareas en la aplicacion, se registran los servicios que realizan operaciones relacionadas con la BD y la logica de la aplicacion

builder.Services.AddScoped<IUsuariosService, UsuariosService>(); 
//builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IRolesService, RolesService>(); 
builder.Services.AddScoped<ISolicitudInscripcionService, SolicitudInscripcionService>();
//builder.Services.AddScoped<INivelEducativoService, NivelEducativoService>();
builder.Services.AddScoped<INoticiasService, NoticiasService>();
builder.Services.AddScoped<IComentariosService, ComentariosService>();

// Cuando el proyecto empiece a crecer, se utiliza los servicios, separandolo de la logica para que los controladores solo llamen al servicio y devolver la logica que el servicio ejecuta   

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Se configura Swagger para documentar la API, que facilita la prueba de Endpoints de la API a traves de una interfaz web

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuracion de Middelwares necesarios para la aplicacion

// Redirecciona las solicitudes a HTTPS para garantizar seguridad
app.UseHttpsRedirection();

// Habilita la autorizacion en la aplicacion
app.UseAuthorization();

// Mapea las rutas de los controladores y permite que la aplicacion responda a las solicitudes HTTP enviadas a los controladores
app.MapControllers();

// Pone en marcha la app web y la mantiene en funcionamiento, escuchando solicitudes entrantes y las redirige a los controladores adecuados
app.Run();
