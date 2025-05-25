using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.DataAccess.Context;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Helpers;

namespace RetailCitikold.Domain.DataAccess.Repositories;

public class RegisterRepository(RetailCitikoldDbContext context, 
    PasswordValidatorLocal passwordValidator, IEmailService emailService,
    TokenHelper genererToken) : IRegisterService
{
    
 
    #region Create Register
    
    public async Task<ProcessResponseDto> CreateRegistrer(Users user)
    {
            try
            {
                if (!passwordValidator.IsValidPassword(user.password, out string error))
                {
                    return new ProcessResponseDto
                    {
                        IsSuccess = false,
                        Mssg = error
                    };
                }
                var passwordHasher = new PasswordHasher<Users>();
                user.password = passwordHasher.HashPassword(user, user.password);
                _ =  await context.Users.AddAsync(user);
                _ =  await context.SaveChangesAsync();
                return new ProcessResponseDto
                {
                    IsSuccess = true,
                    Mssg = ""
                };
            }
            catch (Exception ex)
            {
                return new ProcessResponseDto
                {
                    IsSuccess = false,
                    Mssg = $"Mssg al guardar el item: {ex.InnerException?.Message ?? ex.Message}"
                };
            }
       
    }
    #endregion
  
    #region Read Register
    public async Task<UserResponseDto> ReadRegistre(int id)
    {
        var user = await context.Users.FirstOrDefaultAsync(i => i.id == id);
        if (user == null)
        {
            return new UserResponseDto
            {
                IsSuccess = false,
                Mssg = "Registro no encontrado",
                Users = null
            };
        }
        return new UserResponseDto
        {
            IsSuccess = true,
            Mssg = "Usuario encontrado",
            Users = user
        };
    }
    #endregion

    #region Update Register
    public async Task<ProcessResponseDto> UpdateRegistre(Users usersNew)
    {
        var user = context.Users.FirstOrDefault(i => i.id == usersNew.id);
        if (user == null)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "Usuario no encontrado"
            };
        }
        try
        {
            user.username = usersNew.username; 
            user.fullname = usersNew.fullname;
            // user.email = usersNew.email; 
            user.id_Group = usersNew.id_Group;
            user.id_Estado = usersNew.id_Estado;
            user.image = usersNew.image;
            user.id_Person = usersNew.id_Person;
            await context.SaveChangesAsync();
            return new ProcessResponseDto
            {
                IsSuccess = true,
                Mssg = ""
            };
        }
        catch (Exception ex)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = $"Mssg al editar el usuario: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }
    
    #endregion

    #region Inactive Users
    public async Task<ProcessResponseDto> DeleteRegistre(int id)
    {
        var user = context.Users.FirstOrDefault(i => i.id == id);
        if (user == null)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "Usuario no encontrado"
            };
        }
        try
        {
            user.id_Estado = 2;
            await context.SaveChangesAsync();
            
            return new ProcessResponseDto
            {
                IsSuccess = true,
                Mssg = ""
            };
        }
        catch (Exception ex)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = $"Mssg al Desactivar el User: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }


    #endregion
    
    
    #region Update Password
    
    public async Task<ProcessResponseDto> UpadatePassword(UpdatePasswordRequestDto password)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.username == password.Username);

            if (user == null)
            {
                return new ProcessResponseDto
                {
                    IsSuccess = false,
                    Mssg = "Usuario no encontrado."
                };
            }

            var passwordHasher = new PasswordHasher<Users>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.password, password.CurrentPassword);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return new ProcessResponseDto
                {
                    IsSuccess = false,
                    Mssg = "La contraseña actual es incorrecta."
                };
            }

            
            if (!passwordValidator.IsValidPassword(password.Password, out var validationMssg))
            {
                return new ProcessResponseDto
                {
                    IsSuccess = false,
                    Mssg = validationMssg
                };
            }

            // Hashear la nueva contraseña y guardar
            user.password = passwordHasher.HashPassword(user, password.Password);

            context.Users.Update(user);
            await context.SaveChangesAsync();

            return new ProcessResponseDto
            {
                IsSuccess = true,
                Mssg = ""
            };
        }
        catch (Exception ex)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = $"Mssg al cambiar la contraseña: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }

 
    #endregion
    
#region  Enviar Token de recuperacion por correo
    public async Task<ProcessResponseDto> RestorePassword(RestorePasswordRequestDto email)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.email == email.Email);
        if (user == null)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "Usuario no encontrado."
            };

        }
        var token = genererToken.GeneratePasswordResetToken(user.email);
        
       #region CorreoStyle
             string link = "http://localhost:5185/api/v1/Register/RestorePassword?token=" + token;
       
                           string emailBody = $@"
                    <html>
                    <head>
                      <style>
                        body {{
                          font-family: Arial, sans-serif;
                          background-color: #f4f4f4;
                          margin: 0; padding: 20px;
                        }}
                        .container {{
                          max-width: 600px;
                          margin: 0 auto;
                          background-color: #ffffff;
                          padding: 30px;
                          border-radius: 8px;
                          box-shadow: 0 0 10px rgba(0,0,0,0.1);
                          color: #333333;
                        }}
                        h2 {{
                          color: #007BFF;
                        }}
                        a.button {{
                          display: inline-block;
                          padding: 12px 20px;
                          margin-top: 20px;
                          font-size: 16px;
                          color: #ffffff;
                          background-color: #007BFF;
                          text-decoration: none;
                          border-radius: 5px;
                        }}
                        a.button:hover {{
                          background-color: #0056b3;
                        }}
                        p {{
                          font-size: 16px;
                          line-height: 1.5;
                        }}
                      </style>
                    </head>
                    <body>
                      <div class='container'>
                        <h2>Restablecer tu contraseña</h2>
                        <p>Hola,</p>
                        <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.</p>
                        <p>Por favor, haz clic en el siguiente botón para cambiar tu contraseña:</p>
                        <a href='{link}' class='button'>Restablecer contraseña</a>
                        <p>Si no solicitaste este cambio, ignora este correo.</p>
                        <p>Gracias,<br/>El equipo Testcitikold.</p>
                        <p>Ing. Diego Velarde</p>
                      </div>
                    </body>
                    </html>
                    ";
       
            await emailService.SendAsync(user.email, "Restablecer contraseña", emailBody);
       #endregion
 
        return new ProcessResponseDto
        {
            IsSuccess = true,
            Mssg = $"Se ha enviado un enlace al correo {user.email}"
        };

    }
    #endregion
    
    #region Validar Token

    public async Task<ProcessResponseDto> RestoreChangePassword(ResetPasswordRequestDto restorePassword)
    {
        var tokenHelper = new TokenHelper();
        
        // Validar token y obtener ClaimsPrincipal
        var principal = tokenHelper.ValidatePasswordResetToken(restorePassword.Token);
        
        
        var email = principal.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (email == null)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "No se encontró el email en el token"
            };
        }
        
        var user = await context.Users.FirstOrDefaultAsync(u => u.email == email);
        if (user == null)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "No existe el usuario con ese email"
            };
        }
        
        // Crear instancia de PasswordHasher para la entidad Users
        var passwordHasher = new PasswordHasher<Users>();
        
        // Hashear nueva contraseña y asignarla
        user.password = passwordHasher.HashPassword(user, restorePassword.NewPassword);
        
        await context.SaveChangesAsync();
        
        return new ProcessResponseDto
        {
            IsSuccess = true,
            Mssg = "Clave recuperada exitosamente"
        };
      
        
    }

  

    #endregion

    #region Loggin
   
    public async Task<LogginResponseDto> Loggin(LogginRequestDto loggin)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.email == loggin.Username);
        if (user == null)
        {
            return new LogginResponseDto
            {
                IsSuccess = false,
                Mssg = "Usuario no encontrado",
                Token = null
            };
        }
        if (user.attempts >= 5)
        {
            return new LogginResponseDto
            {
                IsSuccess = false,
                Mssg = "Su Cuenta esta bloqueada por varios intentos fallidos, comuniquese con el administrador",
                Token = null
            };
        }

        var passwordHasher = new PasswordHasher<Users>();
        var result = passwordHasher.VerifyHashedPassword(user, user.password, loggin.Password);

        if (result != PasswordVerificationResult.Success)
        {
            user.attempts++;
            await context.SaveChangesAsync();
            
            int attemptsLeft = 5 - user.attempts;
            return new LogginResponseDto
            {
                IsSuccess = true,
                Mssg = $"Credenciales inválidas. Intentos restantes: {attemptsLeft}",
                Token = null
            };
        }

        if (user.attempts <= 5)
        {
                var token = genererToken.GenerateToken(user.fullname, user.email);
                user.attempts = 0;
                await context.SaveChangesAsync();
                return new LogginResponseDto
                {
                    IsSuccess = true,
                    Mssg = "Ingreso Exitoso",
                    Token = token
                };
        }
        
        return new LogginResponseDto
        {
            IsSuccess = true,
            Mssg = "Loggin Invalido",
            Token = null
        };
    }

    #endregion
    
   
   
}